export const quillInstances = []

document.addEventListener('DOMContentLoaded', function () {
    const forms = document.querySelectorAll('.quill-form')


    forms.forEach(form => {
        const editor = form.querySelector('.editor')
        const input = form.querySelector('.content-input')

        const quill = new Quill(editor, {
            theme: 'snow',
            modules: {
                toolbar: [
                    ['bold', 'italic', 'underline'],
                    [{ 'align': [] }],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    ['link']
                ]
            },
            placeholder: 'Type somthing',
        })
        quillInstances.push({ form, quill })
    })
})