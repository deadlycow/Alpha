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
function removeFromList(element, name) {
    element.remove();
    selectedItems = selectedItems.filter(i => i.name !== name);
}

export const clearList = (form) => {
    selectedItems.length = 0
    const selectedList = form.querySelector('#selectedList')
    if (selectedList)
        selectedList.innerHTML = ''
}