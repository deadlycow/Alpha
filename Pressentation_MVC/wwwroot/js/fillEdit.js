document.querySelectorAll('[data-modal="true"]').forEach(button => {
    button.addEventListener('click', async function () {
        const modal = document.querySelector(this.getAttribute('data-target'))
        const form = modal.querySelector('#form-reg')
        const memberId = this.getAttribute('data-id')

        if (modal)
            modal.style.display = 'flex';

        if (!memberId) return;
        try {

            const response = await fetch(`/Member/GetMember/${memberId}`)
            if (!response.ok)
                throw new Error('Failed to fetch member data');

            const member = await response.json();

            if (member.profileImage) {
                form.querySelector('.image-previewer').classList.add('selected')
                form.querySelector('.image-preview').src = member.profileImage;
            }
            else
                form.querySelector('.image-preview').src = '';

            form.querySelector('[name="Id"]').value = member.id;
            form.querySelector('[name="FirstName"]').value = member.firstName;
            form.querySelector('[name="LastName"]').value = member.lastName;
            form.querySelector('[name="PhoneNumber"]').value = member.phoneNumber;
            form.querySelector('[name="Email"]').value = member.email;
            form.querySelector('[name="JobTitle"]').value = member.jobTitle;
            if (member.address) {
                form.querySelector('[name="Address.City"]').value = member.address.city;
                form.querySelector('[name="Address.Street"]').value = member.address.street;
                form.querySelector('[name="Address.PostalCode"]').value = member.address.postalCode;
            }
            if (member.birthDate) {
                const date = new Date(member.birthDate)
                form.querySelector('[name="Day"]').value = date.getDate()
                form.querySelector('[name="Month"]').value = date.getMonth() + 1
                form.querySelector('[name="Year"]').value = date.getFullYear()
            }

            form.querySelectorAll('input').forEach(input => {
                input.dispatchEvent(new Event('input'))
                input.dispatchEvent(new Event('change'))
            })

        } catch (error) {
            form.innerHTML = `<p>Error loading member data.</p>`;
            console.error(error);
        }
    })
})
