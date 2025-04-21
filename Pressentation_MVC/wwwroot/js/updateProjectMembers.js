document.querySelectorAll('.member-select').forEach(select => {
    select.addEventListener("change", async function () {
        const selectedId = this.value
        const container = this.closest('.relative-container')
        const trigger = container.querySelector('.options-group')
        const projectId = trigger?.getAttribute('data-id')

        if (!projectId || !selectedId) return

        try {
            const resProject = await fetch(`/Project/GetProject/${projectId}`)
            const project = await resProject.json()
            const oldMembers = project.members.map(m => m.id)
            oldMembers.push(selectedId)

            const res = await fetch(`/Members/Update/Project/`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    projectId: projectId,
                    memberIds: oldMembers,
                })
            })
            if (res.ok)
                window.location.reload()
        }
        catch (error) {
            console.log('somthing went wrong')
        }
    })
})