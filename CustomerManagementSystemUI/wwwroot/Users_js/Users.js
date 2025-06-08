document.addEventListener("click", function () {
    const btn = document.getElementById("btnSignUp");
    if (btn) {
        btn.addEventListener("click", function () {
    debugger;
    const user = {
        UserName: document.querySelector("input[name='UserName']").value,
        UserEmail: document.querySelector("input[name='UserEmail']").value,
        Password: document.querySelector("input[name='Password']").value
    };

    fetch('/Users/AddUser', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: JSON.stringify(user)
    })
        .then(response => {
            if (response.ok) {
                alert('User registered successfully!');
                // Optionally, redirect or clear form
            } else {
                return response.json().then(data => {
                    alert('Error: ' + JSON.stringify(data));
                });
            }
        })
                .catch(error => console.error('Error:', error));
        });
    }
});
document.addEventListener("DOMContentLoaded", function () {
    debugger;
    const form = document.getElementById("signinForm");
    debugger;
    if (form) {
        form.addEventListener("submit", function (e) {
            e.preventDefault();
            debugger;
            const token = document.querySelector("input[name='__RequestVerificationToken']");
           // const token = tokenInput ? tokenInput.value : "";

            const loginData = {
                UserEmail: document.querySelector("input[name='UserEmail']").value,
                Password: document.querySelector("input[name='Password']").value
            };

            fetch("/Users/SignIn", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "RequestVerificationToken": token
                },
                body: JSON.stringify(loginData)
            })
                .then(response => {
                    if (!response.ok) throw new Error("Login failed");
                    return response.json();
                })
                .then(data => {
                    window.location.href = "/Home/Home"; 
                })
                .catch(error => {
                    document.getElementById("loginError").textContent = "Check your credentials.";
                    console.error("Error:", error);
                });
        });
    }
});


