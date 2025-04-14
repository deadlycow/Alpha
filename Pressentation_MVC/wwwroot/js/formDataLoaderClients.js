document.addEventListener('DOMContentLoaded', async () => {
    const form = document.querySelector('form');

    try {
        const response = await fetch(`/Project/FormDataLoader/Clients`);
        const data = await response.json();

        const memberSelect = form.querySelector('#clients')
        
        data.clients.forEach(client => {
            const opton = document.createElement('option');
            opton.value = client.id
            opton.textContent = `${client.name}`
            memberSelect.appendChild(opton)

        })
    }
    catch (error) {
        console.error('Error initializing form:', error);
    }
})