document.addEventListener('DOMContentLoaded', () => {
    const daySelect = document.getElementById('day')
    const monthSelect = document.getElementById('month')
    const yearSelect = document.getElementById('year')

    function updateDays() {
        const year = parseInt(yearSelect.value)
        const month = parseInt(monthSelect.value)

        if (!year || !month) return

        const daysInMonth = new Date(year, month, 0).getDate()

        const selectedDay = parseInt(daySelect.value)

        daySelect.innerHTML = '<option value="" disabled selected>Day</options>'
        for (let day = 1; day <= daysInMonth; day++) {
            const option = document.createElement('option')
            option.value = day
            option.textContent = day
            if (day === selectedDay) {
                option.selected = true
            }
            daySelect.appendChild(option)
        }

        if (selectedDay <= daysInMonth) {
            daySelect.value = selectedDay
        }
    }

    monthSelect.addEventListener('change', updateDays)
    yearSelect.addEventListener('change', updateDays)
})