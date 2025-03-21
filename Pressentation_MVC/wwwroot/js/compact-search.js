document.querySelector('[data-compact-search="true"]')
    .addEventListener("click", (event) => {
        event.currentTarget.closest(".compact-search")
             .classList.toggle("active");
    });