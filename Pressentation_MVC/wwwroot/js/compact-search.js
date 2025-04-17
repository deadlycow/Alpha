import { allItems } from './formDataLoaderMembers.js';
export let selectedItems = [];

document.addEventListener("DOMContentLoaded", async () => {
    const forms = document.querySelectorAll('#form-reg-project');

    document.addEventListener("click", (event) => {
        const isInsideSearch = event.target.closest(".compact-search");
        const isInsideDropdown = event.target.closest(".dropdown");
        if (!isInsideSearch && !isInsideDropdown) {
            document.querySelectorAll(".dropdown").forEach(dropdown => {
                dropdown.style.display = 'none';
            })
        }
    });

    forms.forEach(form => {
        const searchInput = form.querySelector("#searchInput")

        searchInput.addEventListener("input", () => filterList(form));
        searchInput.addEventListener("focus", () => filterList(form));
    })
    //forms.forEach(form => {
    //    setupImagePreviewer()




    //    submitBtns.forEach(btn => {
    //        btn.addEventListener('click', async (e) => {
    //            e.preventDefault()
    //            clearErrorMessages(form)

    //            //if (e.target !== form) return

    //            const formData = new FormData(form)

    //            const fileInput = form.querySelector('input[type="file"]')
    //            if (!fileInput.files.length) {
    //                const imagePath = getPreviewImagePath(form)
    //                if (imagePath) {
    //                    formData.append('ProfileImage', imagePath)
    //                }
    //            }
    //            selectedItems.forEach(item => {
    //                formData.append(`MembersId`, item.id)
    //            })

    //            try {
    //                const res = await fetch(form.action, {
    //                    method: 'post',
    //                    body: formData,
    //                })

    //                if (res.ok) {
    //                    const modal = form.closest('.modal')
    //                    if (modal)
    //                        modal.style.display = 'none'
    //                    else
    //                        window.location.reload()
    //                }
    //                else if (res.status === 400) {
    //                    const data = await res.json()

    //                    if (data.errors) {
    //                        Object.keys(data.errors).forEach(key => {
    //                            let input = form.querySelector(`[name="${key}"]`)
    //                            if (input) {
    //                                input.classList.add('input-validation-error')
    //                            }

    //                            let span = form.querySelector(`[data-valmsg-for="${key}"]`)
    //                            if (span) {
    //                                span.innerText = data.errors[key].join('\n')
    //                                span.classList.add('field-validation-error')
    //                            }
    //                        })
    //                    }
    //                }
    //            }
    //            catch {
    //                console.log('error submitting the form')
    //            }
    //        })
    //    })
    //})
})


function filterList(form) {
    let input = form.querySelector("#searchInput").value.toLowerCase();
    let dropdown = form.querySelector("#dropdown");
    dropdown.innerHTML = "";

    let filteredItems = allItems
        .filter(item => item.name.toLowerCase().includes(input))
        .filter(item => !selectedItems.some(selected => selected.name === item.name));

    if (filteredItems.length === 0) {
        dropdown.style.display = "none";
        return;
    }

    filteredItems.forEach(item => {
        let div = document.createElement("div");
        div.textContent = item.name;
        div.onclick = () => addToList(form, item);
        dropdown.appendChild(div);
    });

    dropdown.style.display = "block";
}
export function addToList(form, item) {
    selectedItems.push(item);
    let selectedList = form.querySelector("#selectedList");

    if ([...selectedList.children].some(li => li.dataset.name === item.name)) return;


    let li = document.createElement("li");
    li.dataset.name = item.name;
    li.classList.add("compact-li");

    let profile = document.createElement("img");
    profile.classList.add("compact-li-img");
    profile.src = item.img || "/images/profiles/7.svg";

    let textSpan = document.createElement("span");
    textSpan.textContent = item.name;

    let removeBtn = document.createElement("img");
    removeBtn.classList.add("compact-li-close");
    removeBtn.src = "/images/close-icon.svg";
    removeBtn.onclick = () => removeFromList(li, item.name);

    li.appendChild(profile);
    li.appendChild(textSpan);
    li.appendChild(removeBtn);
    selectedList.appendChild(li);

    form.querySelector("#searchInput").value = "";
    form.querySelector("#dropdown").style.display = "none";
}
function removeFromList(element, item) {
    element.remove();
    selectedItems = selectedItems.filter(i => i !== item);
}
//function clearErrorMessages(form) {
//    form.querySelectorAll('[data-val="true"]').forEach(input => {
//        input.classList.remove('input-validation-error')
//    })

//    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
//        span.innerText = ''
//        span.classList.remove('field-validation-error')
//    })
//}

/*
function filterList() {
    let input = document.getElementById("searchInput").value.toLowerCase();
    let dropdown = document.getElementById("dropdown");
    dropdown.innerHTML = "";

    let filteredItems = allItems
        .filter(item => item.name.toLowerCase().includes(input))
        .filter(item => !selectedItems.some(selected => selected.name === item.name));

    if (filteredItems.length === 0) {
        dropdown.style.display = "none";
        return;
    }

    filteredItems.forEach(item => {
        let div = document.createElement("div");
        div.textContent = item.name;
        div.onclick = () => addToList(item);
        dropdown.appendChild(div);
    });

    dropdown.style.display = "block";
}
function addToList(item) {
    selectedItems.push(item);
    let selectedList = document.getElementById("selectedList");

    if ([...selectedList.children].some(li => li.dataset.name === item.name)) return;


    let li = document.createElement("li");
    li.dataset.name = item.name;
    li.classList.add("compact-li");

    let profile = document.createElement("img");
    profile.classList.add("compact-li-img");
    profile.src = item.img || "/images/profiles/7.svg";

    let textSpan = document.createElement("span");
    textSpan.textContent = item.name;

    let removeBtn = document.createElement("img");
    removeBtn.classList.add("compact-li-close");
    removeBtn.src = "/images/close-icon.svg";
    removeBtn.onclick = () => removeFromList(li, item.name);

    li.appendChild(profile);
    li.appendChild(textSpan);
    li.appendChild(removeBtn);
    selectedList.appendChild(li);

    document.getElementById("searchInput").value = "";
    document.getElementById("dropdown").style.display = "none";
}
function removeFromList(element, item) {
    element.remove();
    selectedItems = selectedItems.filter(i => i !== item);
}
function clearErrorMessages(form) {
    form.querySelectorAll('[data-val="true"]').forEach(input => {
        input.classList.remove('input-validation-error')
    })

    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
        span.innerText = ''
        span.classList.remove('field-validation-error')
    })
}

*/