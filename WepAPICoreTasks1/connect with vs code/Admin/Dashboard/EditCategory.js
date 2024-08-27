let n = Number(localStorage.getItem("CategoryID"));
const url = `https://localhost:44367/api/Categories/${n}`;

var form = document.getElementById("form");

async function editPro() {
    event.preventDefault();
    let formData = new FormData(form);
    console.log(formData);

    let response = await fetch(url, {
        method: 'PUT',
        body: formData,
        // headers: {
        //     'Content-Type': 'application/json'
        // }
    });
    alert("Success");
    ProductName.innerHTML = "";
}


