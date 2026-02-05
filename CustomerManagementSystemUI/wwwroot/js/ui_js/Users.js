document.addEventListener("DOMContentLoaded", function () {
    // ✅ SIGN-IN (keep your existing part)
    const signinForm = document.getElementById("signinForm");
    const btnSignIn = document.getElementById("btnSignIn");

    if (btnSignIn && signinForm) {
        btnSignIn.addEventListener("click", async function (event) {
            event.preventDefault();

            const formData = new FormData(signinForm);

            try {
                const response = await fetch("/Users/SignIn", {
                    method: "POST",
                    body: formData
                });

                const result = await response.json();

                if (result.success) {
                    alert(result.message);
                    window.location.href = result.redirectUrl;
                } else {
                    document.getElementById("loginError").innerText = result.message;
                }
            } catch (error) {
                console.error("Error:", error);
                document.getElementById("loginError").innerText = "Something went wrong.";
            }
        });
    }

    // ✅ SIGN-UP (Fixed version)
    const signUpForm = document.getElementById("SignUpForm");
    const btnSignUp = document.getElementById("btnSignUp");

    if (btnSignUp && signUpForm) {
        signUpForm.addEventListener("submit", async function (event) {
            event.preventDefault();

            const user = {
                UserName: document.querySelector("input[name='UserName']").value.trim(),
                UserEmail: document.querySelector("input[name='UserEmail']").value.trim(),
                Password: document.querySelector("input[name='Password']").value.trim()
            };

            try {
                const response = await fetch("/Users/AddUser", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]')?.value || ""
                    },
                    body: JSON.stringify(user)
                });

                if (response.redirected) {
                    // If server redirects (successful user added)
                    window.location.href = response.url;
                    return;
                }

                const result = await response.json();

                if (response.ok) {
                    alert(result.message || "User registered successfully!");
                    window.location.href = "/Users/SignIn";
                } else {
                    document.getElementById("loginError").innerText = result.message || "Failed to register user.";
                }
            } catch (error) {
                console.error("Error:", error);
                document.getElementById("loginError").innerText = "Something went wrong while signing up.";
            }
        });
    }
});



//document.addEventListener("DOMContentLoaded", function () {
//    const signinForm = document.getElementById("signinForm");
//    const btnSignIn = document.getElementById("btnSignIn");

//    if (btnSignIn && signinForm) {
//        btnSignIn.addEventListener("click", async function (event) {
//            event.preventDefault();

//            const formData = new FormData(signinForm);

//            try {
//                const response = await fetch("/Users/SignIn", {
//                    method: "POST",
//                    body: formData // ✅ send FormData
//                });

//                const result = await response.json(); // ✅ expects JSON response

//                if (result.success) {
//                    window.location.href = result.redirectUrl;
//                    alert(result.message);
//                } else {
//                    document.getElementById("loginError").innerText = result.message;
//                }
//            } catch (error) {
//                console.error("Error:", error);
//                document.getElementById("loginError").innerText = "Something went wrong.";
//            }
//        });
//    }
//});

//document.addEventListener("click", function () {
//    const btn = document.getElementById("btnSignUp");
//    if (btn) {
//        btn.addEventListener("click", function () {
//            debugger;
//            const user = {
//                UserName: document.querySelector("input[name='UserName']").value,
//                UserEmail: document.querySelector("input[name='UserEmail']").value,
//                Password: document.querySelector("input[name='Password']").value
//            };

//            fetch('/Users/AddUser', {
//                method: 'POST',
//                headers: {
//                    'Content-Type': 'application/json',
//                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
//                },
//                body: JSON.stringify(user)
//            })
//                .then(response => {
//                    if (response.ok) {
//                        alert('User registered successfully!');
//                        // Optionally, redirect or clear form
//                    } else {
//                        return response.json().then(data => {
//                            alert('Error: ' + JSON.stringify(data));
//                        });
//                    }
//                })
//                .catch(error => console.error('Error:', error));
//        });
//    }
//});

