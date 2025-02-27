let postingPrompt = ""; // AI-ya göndəriləcək prompt
let selectedColor = ""; // Seçilmiş rəng

// Promptu yaratmaq üçün funksiya
function generatePrompt() {
    if (selectedColor) {
        postingPrompt = `I want to make a combine for tomorrow, based on this color ${selectedColor}.`;
    } else {
        postingPrompt = "I want to make a combine for tomorrow.";
    }
}

// Seçilmiş rəngi izləmək üçün hadisə dinləyicisi
document.querySelectorAll('input[name="clothColor"]').forEach(radio => {
    radio.addEventListener('change', () => {
        selectedColor = radio.value; // Seçilmiş rəngi yenilə
    });
});

// "Generate Prompt" düyməsinə tıklananda funksiyanı çağırın
document.getElementById('generatePrompt').addEventListener('click', async () => {
    const postingPrompt = "I want to make a combine for tomorrow, based on this color green.";
    
    try {
        const response = await fetch('http://localhost:5158/api/prompt/generate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer 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` // Buraya öz API açarınızı daxil edin
            },
            body: JSON.stringify({
                prompt: postingPrompt // Modelə göndəriləcək prompt
            })
        });

        if (!response.ok) {
            throw new Error(`AI serverindən cavab alınmadı: ${response.statusText}`);
        }

        const data = await response.text();  // JSON əvəzinə plain text oxumaq
        console.log("Server Cavabı:", data);  // Konsolda serverin döndərdiyi raw cavabı yazdırın

        try {
            const jsonData = JSON.parse(data); // Əgər JSON varsa, onu parse edin
            console.log("JSON Cavabı:", jsonData);
        } catch (jsonError) {
            console.error("JSON formatına çevrilə bilmədi:", jsonError);
        }
    } catch (error) {
        console.error("Xəta:", error);
    }
});
