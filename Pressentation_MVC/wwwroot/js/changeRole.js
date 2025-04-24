document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.member-card-role').forEach(button => {
        button.addEventListener('click', function (event) {
            event.stopPropagation();

            const dropdown = this.closest('.member-card').querySelector('.dropdown-roles');

            document.querySelectorAll('.dropdown-roles').forEach(d => {
                if (d.style.display === 'block') {
                    d.style.display = 'none'
                }
            })
            dropdown.style.display = 'block'
        })
    })

    document.querySelectorAll('.dropdown-roles li').forEach(item => {
        item.addEventListener('click', async function (event) {
            event.stopPropagation();

            const selectedRole = this.getAttribute('data-role')
            const memberId = this.closest('.member-card').getAttribute('data-id')
            try {
                const res = await fetch(`Members/${memberId}/role`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify( selectedRole )
                })
                if (res.ok) {
                    window.location.reload()
                }
                else {
                    console.log('Failed to update role')
                }
            }
            catch (error) {
                console.error('Error:', error)
            }
        })
    })

    document.addEventListener('click', function () {
        document.querySelectorAll('.dropdown-roles').forEach(d => {
            d.style.display = 'none'
        })
    })
})