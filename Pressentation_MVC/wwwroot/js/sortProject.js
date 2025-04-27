document.addEventListener('DOMContentLoaded', () => {
	const projectCard = document.querySelectorAll('.project-card');
	const filterOptions = document.querySelectorAll('.options-list li');
	filterOptions.forEach(option => {
		option.addEventListener('click', () => {
			const filterValue = option.getAttribute('data-filter');

			filterOptions.forEach(opt => opt.classList.remove('active'));
			option.classList.add('active');

			projectCard.forEach(card => {
				const startDate = card.getAttribute('data-start-date')
				const endDate = card.getAttribute('data-end-date')
				const status = card.getAttribute('data-status')
				const currentDate = new Date().toISOString().split('T')[0];

				let show = false;

				if (filterValue === 'all') {
					show = true
				}
				else if (filterValue == 'started') {
					if (startDate <= currentDate && (!endDate || endDate >= currentDate))
						show = true;
				}
				else if (filterValue === 'completed') {
					if (status === 'True')
						show = true
				}
				else if (filterValue === 'expired') {
					if (endDate && endDate < currentDate && status === 'False')
						show = true
				}
				card.style.display = show ? 'grid' : 'none';
			})
		})
	})
})