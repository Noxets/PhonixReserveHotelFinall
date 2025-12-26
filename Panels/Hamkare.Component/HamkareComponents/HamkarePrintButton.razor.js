window.Pdf = {
    generatePDF: async function (model) {
        try {
            let element = $(model.pdfSetting.element);
            let body = document.body;
            let html = document.documentElement;
            let height = Math.max(body.scrollHeight, body.offsetHeight,
                html.clientHeight, html.scrollHeight, html.offsetHeight);
            let heightCM = height / 35.35;

            var opt = {
                margin: 1,
                enableLinks: true,
                filename: model.pdfSetting.fileName,
                html2canvas: { dpi: 192, letterRendering: true, allowTaint: true, useCORS: true },
                jsPDF: {
                    orientation: 'portrait',
                    unit: 'cm',
                    format: [heightCM, 1360 / 35.35],
                    compress: true
                }
            };

            html2pdf().set(opt).from(element).save();
        } catch (error) {
            console.error("Error generating PDF:", error);
        }
    }
};