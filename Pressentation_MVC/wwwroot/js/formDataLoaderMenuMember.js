document.querySelectorAll('[data-menu-modal="true"').forEach(button => {
    button.addEventListener('click', async function (event) {
        event.preventDefault()
        event.stopPropagation()
         
        const container = this.closest('.relative-container')
        const modal = container.querySelector('.member-list')
        const select = modal.querySelector('select.member-select')
        const projectId = this.getAttribute('data-id')

        if (!projectId) return

        modal.style.display = 'block'

        try {
            const resProject = await fetch(`/Project/GetProject/${projectId}`)
            const resMembers = await fetch(`/Members/GetAll`)

            if (!resProject.ok)
                throw new Error('Failed to fetch project data')

            const members = await resMembers.json()
            const project = await resProject.json();

            select.innerHTML = ''

            const existingMemberIds = new Set(project.members.map(m => m.id))
            const filteredMembers = members.filter(member => !existingMemberIds.has(member.id))

            filteredMembers.forEach(member => {
                const option = document.createElement('option')
                option.value = member.id
                option.textContent = member.name
                select.appendChild(option)
            })
        }
        catch (error) {
            select.innerHTML = `<p>Error loading members</p>`
            console.log(error)
        }
    })
})