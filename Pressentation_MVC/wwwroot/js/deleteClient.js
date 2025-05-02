document.addEventListener('DOMContentLoaded', () => {
    const btns = document.querySelectorAll('[data-client-delete="true"]')

    btns.forEach(btn => {
        btn.addEventListener('click', async (event) => {
            event.stopPropagation()

            const id = event.currentTarget.getAttribute('data-id')

            try {
                const res = await fetch(`/clients/delete/${id}`, {
                    method: 'POST',
                })
                if (res.ok)
                    window.location.reload()
            }
            catch (error) {
                console.error(`Error deleting Client with id:${id}`, error)
            }
        })
    })
})