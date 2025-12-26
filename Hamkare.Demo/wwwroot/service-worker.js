self.addEventListener('fetch', () => {
})

self.addEventListener('install', function (event) {
    // The promise that skipWaiting() returns can be safely ignored.
    self.skipWaiting()

    // Perform any other actions required for your
    // service worker to install, potentially inside
    // of event.waitUntil();
})

self.addEventListener('push', event => {
    const payload = event.data.json()
    self.registration.showNotification(payload.title, {
        body: payload.message,
        icon: payload.icon,
        vibrate: [100, 50, 100],
        data: {url: payload.url},
    })
})

self.addEventListener('notificationclick', event => {
    event.notification.close()
    event.waitUntil(clients.openWindow(event.notification.data.url))
})