document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.input-complete-project').forEach(input => {
        input.addEventListener('click', async function (event) {
            event.stopPropagation();
            let id = Number(this.getAttribute('data-id'));
            let isCompleted = this.checked;

            try {
                const res = await fetch(`/Project/Update/${id}/${isCompleted}`, {
                    method: 'POST'
                })
                if (res.ok) {
                    console.log('Project completion status updated successfully.');
                } else {
                    console.error('Failed to update project completion status.');
                }
            }
            catch (error) {
                console.error('Error:', error);
            }
        })
    })
})