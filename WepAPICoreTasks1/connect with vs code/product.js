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
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-4">
                    <div class="card shadow-lg">
                        <img src="../images/${element.productImage}" class="card-img-top" alt="Card image">
                        <div class="card-body text-center">
                            <h3  class="card-title">${element.productId}</h3>
                            <h5 class="card-title">${element.productName}</h5>
                            <p class="card-text">${element.description}</p>
                            
                            <a class="btn btn-primary" href="details.html" onclick="store(${element.productId})">Details</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-4">
                    <div class="card shadow-lg">
                        <img src="../images/${element.productImage}" class="card-img-top" alt="Card image">
                        <div class="card-body text-center">
                            <h3  class="card-title">${element.productId}</h3>
                            <h5 class="card-title">${element.productName}</h5>
                            <p class="card-text">${element.description}</p>
                            
                            <a class="btn btn-primary" href="details.html" onclick="store(${element.productId})">Store Data</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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

