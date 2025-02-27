let postingPrompt = ""; // Ai a Gedecek Olan Promt
let selectedColor = ""; 
const generatePrompt = document.getElementById('generatePrompt');
const generatedPrompt = document.getElementById('generatedPrompt');
function generatePromt() {
    if(selectedColor){
        postingPrompt = `i want to make a combine for tomorrow, based on this color ${selectedColor}.`;
    }else{
        postingPrompt = "i want to make a combine for tomorrow";
    }
}

document.querySelectorAll('input[name="clothColor"]').forEach(radio => {
    radio.addEventListener('change', () => {
        selectedColor=radio.value;
    });
});

generatePrompt.addEventListener('click', () => {
    generatePromt();
    generatedPrompt.innerHTML = postingPrompt;
});

// postingPrompt BUNU AI YA GONDERMEK LAZIM