document.addEventListener('DOMContentLoaded', async () => {

    const forms = document.querySelectorAll('#form-reg-project');
    try {
        const response = await fetch(`/Project/FormDataLoader/Clients`);
        const data = await response.json();

        forms.forEach(form => {
            const memberSelect = form.querySelector('#clients')

            data.clients.forEach(client => {
                const option = document.createElement('option');
                option.value = client.id
                option.textContent = `${client.name}`
                memberSelect.appendChild(option)
            })
        })
    }
    catch (error) {
        console.error('Error initializing form:', error);
    }
})