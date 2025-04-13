let allItems = [];
let selectedItems = [];

document.addEventListener("DOMContentLoaded", async () => {
    const result = await fetch('/Project/FormDataLoader/Members');
    const data = await result.json();

    allItems = data.members.map(member => ({ name: `${member.firstName} ${member.lastName}`, id: member.id, img: member.profileImage }))

    document.addEventListener("click", (event) => {
        if (!event.target.closest(".compact-search")) {
            if (dropdown)
                dropdown.style.display = 'none';
        }
    });
})
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
    selectedItems = selectedItems.filter(i => i !== item.name);
}

