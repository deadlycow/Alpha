const allItems = ["Andreas Karlsson", "Anders Andersson", "Oskar Oskarsson", "Lina Linsson", "Karin Karlsson", "Erik Eriksson"];
let selectedItems = [];

function filterList() {
    let input = document.getElementById("searchInput").value.toLowerCase();
    let dropdown = document.getElementById("dropdown");
    dropdown.innerHTML = "";

    let filteredItems = allItems
        .filter(item => item.toLowerCase().includes(input))
        .filter(item => !selectedItems.includes(item));

    if (filteredItems.length === 0) {
        dropdown.style.display = "none";
        return;
    }

    filteredItems.forEach(item => {
        let div = document.createElement("div");
        div.textContent = item;
        div.onclick = () => addToList(item);
        dropdown.appendChild(div);
    });

    dropdown.style.display = "block";
}

function addToList(name) {
    selectedItems.push(name);
    let selectedList = document.getElementById("selectedList");

    if ([...selectedList.children].some(li => li.dataset.name === name)) return;


    let li = document.createElement("li");
    li.dataset.name = name;
    li.classList.add("compact-li");

    let profile = document.createElement("img");
    profile.classList.add("compact-li-img");
    profile.src = "/images/profiles/7.svg";

    let textSpan = document.createElement("span");
    textSpan.textContent = name;

    let removeBtn = document.createElement("img");
    removeBtn.classList.add("compact-li-close");
    removeBtn.src = "/images/close-icon.svg";
    removeBtn.onclick = () => removeFromList(li, name);

    li.appendChild(profile);
    li.appendChild(textSpan);
    li.appendChild(removeBtn);
    selectedList.appendChild(li);

    document.getElementById("searchInput").value = "";
    document.getElementById("dropdown").style.display = "none";
}

function removeFromList(element, name) {
    element.remove();
    selectedItems = selectedItems.filter(i => i !== name);
}

document.addEventListener("click", (event) => {
    if (!event.target.closest(".compact-search")) {
        if (dropdown)
            dropdown.style.display = 'none';
    }
});