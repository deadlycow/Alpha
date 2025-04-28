document.addEventListener('DOMContentLoaded', function () {
    const forms = document.querySelectorAll('.quill-form')

    const quillInstances = []

    forms.forEach(form => {
        const editor = form.querySelector('.editor')
        const input = form.querySelector('.content-input')

        const quill = new Quill(editor, {
            theme: 'snow',
            modules: {
                toolbar: [
                    ['bold', 'italic', 'underline'],
                    [{ 'list': 'ordered' }, { 'list:': 'bullet' }]
                ]
            }
        })
        quillInstances.push(quill)

        form.addEventListener('submit', function (e) {
            input.value = quill.root.innerHTML
        })
    })
})