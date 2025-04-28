document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('searchInput')
    const dataTarget = document.querySelector('div[data-search-target]').getAttribute('data-search-target')

    const items = document.querySelectorAll(`.${dataTarget}`)

    searchInput.addEventListener('change', function () {
        const searhTerm = this.value.toLowerCase()

        items.forEach(item => {
            const name = item.getAttribute('data-value-1').toLowerCase()
            const email = item.getAttribute('data-value-2').toLowerCase()

            if (name.includes(searhTerm) || email.includes(searhTerm))
                item.style.display = 'grid'
            else 
                item.style.display = 'none'
        })
    })
})