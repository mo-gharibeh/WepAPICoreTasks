
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



let n = Number(localStorage.getItem("ProductID"));
const url1 = `https://localhost:44367/api/Products/${n}`;

var form = document.getElementById("form");

async function editPro() {
    event.preventDefault();
    let formData = new FormData(form);
    console.log(formData);

    let response = await fetch(url1, {
        method: 'PUT',
        body: formData,
        // headers: {
        //     'Content-Type': 'application/json'
        // }
    });
    alert("Success");
    ProductName.innerHTML = "";
}
