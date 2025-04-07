document.querySelectorAll('[data-modal="true"]').forEach(button => {
    button.addEventListener('click', async function () {
        const modal = document.querySelector(this.getAttribute('data-target'))
        const form = modal.querySelector('form')
        const memberId = this.getAttribute('data-id')

        if (!memberId) return;
        try {
            const response = await fetch(`/Member/GetMember/${memberId}`)
            if (!response.ok)
                throw new Error('Failed to fetch member data');

            const member = await response.json();
            console.log(member)
            form.querySelector('[name="Id"]').value = member.id;
            form.querySelector('[name="FirstName"]').value = member.firstName;
            form.querySelector('[name="LastName"]').value = member.lastName;
            form.querySelector('[name="PhoneNumber"]').value = member.phoneNumber;
            form.querySelector('[name="Email"]').value = member.email;
            form.querySelector('[name="JobTitle"]').value = member.jobTitle;
            form.querySelector('[name="Address"]').value = member.address;

        } catch (error) {
            form.innerHTML = `<p>Error loading member data.</p>`;
            console.error(error);
        }
    })
})