export let allItems = []

document.addEventListener("DOMContentLoaded", async () => {
  const result = await fetch('/Project/FormDataLoader/Members');
  const data = await result.json();

  allItems = data.members.map(member => ({ name: member.name, id: member.id, img: member.profileImage }))
})