document.addEventListener('DOMContentLoaded', async () => {

    const forms = document.querySelectorAll('#form-reg-project');
    try {
        const response = await fetch(`/Project/FormDataLoader/Clients`);
        const data = await response.json();

        forms.forEach(form => {
        const memberSelect = form.querySelector('#clients')

            data.clients.forEach(client => {
                const opton = document.createElement('option');
                opton.value = client.id
                opton.textContent = `${client.name}`
                memberSelect.appendChild(opton)

            })
        })
    }
    catch (error) {
        console.error('Error initializing form:', error);
    }
})