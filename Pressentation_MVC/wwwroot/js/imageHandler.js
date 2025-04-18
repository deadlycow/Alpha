
const loadImage = async (file) => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onerror = () => reject(new Error("Failed to load file."));
        reader.onload = (e) => {
            const img = new Image();
            img.onerror = () => reject(new Error("Failed to load image "));
            img.onload = () => resolve(img);
            img.src = e.target.result;
        }
        reader.readAsDataURL(file);
    })
}

const processImage = async (file, imagePreviewer, previewer, previewSize = 150) => {
    try {
        const img = await loadImage(file);
        const canvas = document.createElement('canvas');
        canvas.width = previewSize;
        canvas.height = previewSize;

        const ctx = canvas.getContext('2d');
        ctx.drawImage(img, 0, 0, previewSize, previewSize);
        imagePreviewer.src = canvas.toDataURL('image/jpeg');
        previewer.classList.add('selected');
    }
    catch (error) {
        console.error('Failed on image-processing:', error);
    }
}

 const setupImagePreviewer = (previewSize = 150) => {
    document.querySelectorAll(".image-previewer").forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]');
        const imagePreview = previewer.querySelector('.image-preview');

        previewer.addEventListener('click', () => fileInput.click())

        fileInput.addEventListener('change', ({ target: { files } }) => {
            const file = files[0];
            if (file)
                processImage(file, imagePreview, previewer, previewSize)
        })
    })
}

document.addEventListener('DOMContentLoaded', () => {
    setupImagePreviewer()
})

export const resetImagePreview = (form) => {
    const imagePreview = form.querySelector('.image-preview')
    if (imagePreview) {
        imagePreview.src = ''
        form.querySelector('.image-previewer').classList.remove('selected')
    }
}

export const getPreviewImagePath = (form) => {

    const previewImage = form.querySelector('.image-preview')

    if (previewImage || previewImage.src)
        try {

            const url = new URL(previewImage.src)
            const isInUploadsFolder = url.pathname.startsWith('/images/uploads/')
            const isImageFile = /\.(svg|jpg|jpeg|png|gif|webp)$/i.test(url.pathname)
            if (isInUploadsFolder && isImageFile) {
                return url.pathname
            }
        } catch (error) {
            console.error('Ogiltig URL på bild:', error);
        }
}



