
const url = "https://localhost:44367/api/Categories"

async function getCategory() {

    let response = await fetch(url);
    var result = await response.json();

    const dropdown = document.getElementById('CategoryId');
    debugger
    // Populate the dropdown with options
    result.forEach(category => {
        const option = document.createElement('option');
        option.value = category.categoryId;
        option.innerHTML = category.categoryName;
        dropdown.appendChild(option);
    });
}

getCategory();
debugger

const url1 = "https://localhost:44367/api/Products"

let form = document.getElementById("form");
async function save(){
    debugger
    event.preventDefault();
    let formData = new FormData(form);
    let request = await fetch(url1,{method:"POST", body: formData});
    alert("Success  saved");
}