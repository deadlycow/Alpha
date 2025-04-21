import { addToList } from './compact-search.js'

document.querySelectorAll('[data-modal="true"').forEach(button => {
    button.addEventListener('click', async function () {
        const modal = document.querySelector(this.getAttribute('data-target'))
        const form = modal.querySelector('form')
        const projectId = this.getAttribute('data-id')

        if (!projectId) return

        try {
            const response = await fetch(`/Project/GetProject/${projectId}`)

            if (!response.ok)
                throw new Error('Failed to fetch project data')

            const project = await response.json();

            if (project.projectImage) {
                form.querySelector('.image-previewer').classList.add('selected')
                form.querySelector('.image-preview').src = project.projectImage;
            }
            else
                form.querySelector('.image-preview').src = '';

            form.querySelector('[name="Id"]').value = project.id
            form.querySelector('[name="Name"]').value = project.name
            form.querySelector('[name="Description"]').value = project.description
            form.querySelector('[name="StartDate"]').value = project.startDate
            form.querySelector('[name="EndDate"]').value = project.endDate
            form.querySelector('[name="ClientId"]').value = project.clientId
            form.querySelector('[name="Budget"]').value = project.budget

            project.members.forEach(member => {
                addToList(form, {
                    name: member.name,
                    id: member.id,
                    img: member.profileImage,
                })
            })
        }
        catch (error) {
            form.innerHTML = `<p>Error loading project data</p>`
            console.log(error)
        }
    })
})