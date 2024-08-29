const url = "https://localhost:44367/api/CartItem"
debugger
async function getCartItems() {

    let response = await fetch(url);
    var result = await response.json();

    console.log(response);

    var container = document.getElementById("container");

    result.forEach(element =>{
        container.innerHTML += 
        `
        <tr>
            <td>${element.cartId}</td>
            <td>${element.product.productName}</td>
            <td><input type="number" name="quantity" id="quantity" value="" placeholder="${element.quantity}" ></input></td>
            <td><a class="btn btn-primary"  onclick="edit(${element.cartItemId})">Edit</a></td>
            <td><a class="btn btn-primary"  onclick="delet(${element.cartItemId})">Delete</a></td>
        </tr>
        <!-- Add more rows as needed -->
                    
        `
       
    })

}

async function edit(id){
    debugger;
    event.preventDefault();

    const url = `https://localhost:44367/api/CartItem/UpdateItem/${id}`;
    let quantity = document.getElementById("quantity");
    let container = document.getElementById("container");
    var request = {
        quantity: quantity.value,
    }

    let response = await fetch(url, {
        method: 'PUT',
        body : JSON.stringify(request),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    alert("Item updated successfully");

}

async function delet(id){
    debugger;
    event.preventDefault();
    const url = `https://localhost:44367/api/CartItem/DeleteItem/${id}`;
    let container = document.getElementById("container");
    var request = {
        cartItemId: id,
    }
    let response = await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
        // body : JSON.stringify(request),
        
        

        
    });
    alert("Item deleted successfully");
}

// function store(id){
//     localStorage.setItem("cartItemId",id);
// }

function reset(){
    localStorage.clear();
}

getCartItems();