document.querySelectorAll('[data-modal="true"').forEach(button => {
    button.addEventListener('click', async function () {
        const modal = document.querySelector(this.getAttribute('data-target'))
        const form = modal.querySelector('#form-reg-project')
        const projectId = this.getAttribute('data-id')

        if (!projectId) return

        try {
            const response = await fetch(`/Project/GetProject/${projectId}`)
            
            if (!response.ok)
                throw new Error('Failed to fetch project data')

            const project = await response.json();

            form.querySelector('[name="Id"]').value = project.id
        }
        catch (error) {
            form.innerHTML = `<p>Error loading project data</p>`
            console.log(error)
        }

    })
})