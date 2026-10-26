document.addEventListener("DOMContentLoaded", function () {

    const loginForm = document.getElementById("loginForm");

    if (!loginForm)
        return;

    loginForm.addEventListener("submit", async function (e) {

        e.preventDefault();

        const loginMessage =
            document.getElementById("loginMessage");

        const btnLogin =
            document.getElementById("btnLogin");

        const btnLoginText =
            document.getElementById("btnLoginText");

        // پاک کردن پیام قبلی
        loginMessage.classList.add("hidden");
        loginMessage.classList.remove(
            "text-red-600",
            "bg-red-50"
        );
        loginMessage.textContent = "";

        // غیرفعال کردن دکمه
        btnLogin.disabled = true;
        btnLoginText.textContent = "در حال ورود...";

        try {

            const response = await fetch(
                loginForm.action,
                {
                    method: "POST",
                    body: new FormData(loginForm)
                }
            );

            if (!response.ok) {
                throw new Error("Server error");
            }

            const result = await response.json();

            if (result.success) {

                // ورود موفق
                location.reload();

            } else {

                // ورود ناموفق
                loginMessage
                    .classList
                    .remove("hidden");

                loginMessage
                    .classList
                    .add(
                        "text-red-600",
                        "bg-red-50"
                    );

                loginMessage.textContent =
                    result.message;

                btnLogin.disabled = false;
                btnLoginText.textContent = "ورود";
            }

        }
        catch (error) {

            loginMessage
                .classList
                .remove("hidden");

            loginMessage
                .classList
                .add(
                    "text-red-600",
                    "bg-red-50"
                );

            loginMessage.textContent =
                "ارتباط با سرور برقرار نشد.";

            btnLogin.disabled = false;
            btnLoginText.textContent = "ورود";
        }

    });

});