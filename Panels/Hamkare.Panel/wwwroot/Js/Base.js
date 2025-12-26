(() => {
    const maximumRetryCount = 600;
    const retryIntervalMilliseconds = 100;
    const reconnectModal = document.getElementById('reconnect-modal');

    const startReconnectionProcess = () => {
        reconnectModal.style.display = 'block';

        let isCanceled = false;

        (async () => {
            for (let i = 0; i < maximumRetryCount; i++) {
                reconnectModal.innerText = `Attempting to reconnect: ${i + 1} of ${maximumRetryCount}`;

                await new Promise(resolve => setTimeout(resolve, retryIntervalMilliseconds));

                if (isCanceled) {
                    return;
                }

                try {
                    const result = await Blazor.reconnect();
                    if (!result) {
                        // The server was reached, but the connection was rejected; reload the page.
                        location.reload();
                        return;
                    }

                    // Successfully reconnected to the server.
                    return;
                } catch {
                    // Didn't reach the server; try again.
                }
            }

            // Retried too many times; reload the page.
            location.reload();
        })();

        return {
            cancel: () => {
                isCanceled = true;
                reconnectModal.style.display = 'none';
            },
        };
    };

    let currentReconnectionProcess = null;

    Blazor.start({
        reconnectionHandler: {
            onConnectionDown: () => currentReconnectionProcess ??= startReconnectionProcess(),
            onConnectionUp: () => {
                currentReconnectionProcess?.cancel();
                currentReconnectionProcess = null;
            }
        },
        reconnectionOptions: {
            maxRetries: maximumRetryCount,
            retryIntervalMilliseconds: retryIntervalMilliseconds
        },
        configureSignalR: function (builder) {
            builder.withServerTimeout(57600000).withKeepAliveInterval(28800000);
        }
    });

    let lockResolver;
    if (navigator && navigator.locks && navigator.locks.request) {
        const promise = new Promise((res) => {
            lockResolver = res;
        });

        navigator.locks.request('Hamkare_Lock', {mode: "shared"}, () => {
            return promise;
        });
    }
})();

//#region ServiceWorker

navigator.serviceWorker.register('/service-worker.js');

(async function () {
    const applicationServerPublicKey = 'BB9Ezvjst_KyHdA4a8cynUsL-Th2San4MCGqIjUUOFNtr7mp5-SNGzdlt4TDNfW646Y7ialVAklQBhKPSuf3hbE';

    window.blazorPushNotifications = {
        requestSubscription: async () => {
            const worker = await navigator.serviceWorker.getRegistration();
            if (!worker) {
                console.error('Service worker not registered');
                return;
            }

            try {
                const permissionState = await Notification.requestPermission();
                if (permissionState !== 'granted') {
                    console.log('User denied notification permission');
                    return;
                }

                const existingSubscription = await worker.pushManager.getSubscription();
                if (!existingSubscription) {
                    const newSubscription = await subscribe(worker);
                    if (newSubscription) {
                        return {
                            url: newSubscription.endpoint,
                            p256dh: arrayBufferToBase64(newSubscription.getKey('p256dh')),
                            auth: arrayBufferToBase64(newSubscription.getKey('auth')),
                        };
                    }
                }
            } catch (error) {
                console.error('Error requesting permission:', error);
            }
        },

        unSubscribe: async () => {
            const worker = await navigator.serviceWorker.getRegistration();
            const existingSubscription = await worker.pushManager.getSubscription();
            if (existingSubscription) {
                existingSubscription.unsubscribe();
                return true;
            }
        },
    };

    async function subscribe(worker) {
        try {
            return await worker.pushManager.subscribe({
                userVisibleOnly: true,
                applicationServerKey: applicationServerPublicKey,
            });
        } catch (error) {
            if (error.name === 'NotAllowedError') {
                return null;
            }
            throw error;
        }
    }

    function arrayBufferToBase64(buffer) {
        let binary = '';
        const bytes = new Uint8Array(buffer);
        const len = bytes.byteLength;
        for (let i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }
        return window.btoa(binary);
    }
})();

//#endregion