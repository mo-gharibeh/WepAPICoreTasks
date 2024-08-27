const n = localStorage.getItem("CategoryID");

const url = `https://localhost:44367/api/Products/category/${n}`;

async function getCategory() {

    let response = await fetch(url);
    var result = await response.json();

    console.log(response);

    var container = document.getElementById("container");

    result.forEach(element =>{
        container.innerHTML += 
        `
        <tr>
            <td><img src="../images/${element.productImage}" alt="Product Image" class="img-fluid" style="max-width: 100px;"></td>
            <td>${element.productId}</td>
            <td>${element.productName}</td>
            <td>${element.description}</td>
            <td><a class="btn btn-primary" href="details.html" onclick="store(${element.productId})">Details</a></td>
        </tr>
        `
       
    })

}

const url1 = "https://localhost:44367/api/Products";


async function getAllProduct() {

    let response = await fetch(url1);
    var result = await response.json();

    console.log(response);

    var container = document.getElementById("container");

    result.forEach(element =>{
        container.innerHTML += 
        `
        <tr>
        <td>${element.productId}</td>
        <td>${element.productName}</td>
        <td>${element.price}</td>
        <td><img src="../images/${element.productImage}" alt="Product Image" class="img-fluid" style="max-width: 100px;"></td>
            <td>${element.description}</td>
            <td><a class="btn btn-primary" href="EditProduct.html" onclick="store(${element.productId})">Edit</a></td>
        </tr>
        `
    })

}

function reset(){
    localStorage.clear();
}

function store(id){
    localStorage.setItem("ProductID",id);
}

if ( localStorage.getItem("CategoryID") == null){
    getAllProduct();
}
else {
    getCategory();
}

