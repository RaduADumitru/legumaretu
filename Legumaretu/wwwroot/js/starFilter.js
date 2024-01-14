document.addEventListener("DOMContentLoaded", () => {
    // Uncheck all inputs on content load
    document.querySelectorAll(".starfilter input").forEach(input => input.checked = false);

    // When clicking on an input,
    // uncheck the others, then check it
    document.querySelectorAll(".starfilter input").forEach(input => {
        input.addEventListener("click", () => {
            document.querySelectorAll(".starfilter span").forEach(span => {
                span.classList.remove("checked");
            });

            input.parentElement.classList.add("checked");
        });
    });
});