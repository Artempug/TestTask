function validSolution(board) {
    // Допоміжна функція для перевірки, чи містить масив всі цифри від 1 до 9
    function isValidSet(arr) {
        const sorted = arr.slice().sort();
        return sorted.length === 9 && 
               sorted.every((val, i) => val === i + 1);
    }
    
    // Перевіряємо рядки
    for (let i = 0; i < 9; i++) {
        if (!isValidSet(board[i])) {
            return false;
        }
    }
    
    // Перевіряємо стовпці
    for (let col = 0; col < 9; col++) {
        const column = [];
        for (let row = 0; row < 9; row++) {
            column.push(board[row][col]);
        }
        if (!isValidSet(column)) {
            return false;
        }
    }
    
    // Перевіряємо блоки 3x3
    for (let blockRow = 0; blockRow < 3; blockRow++) {
        for (let blockCol = 0; blockCol < 3; blockCol++) {
            const block = [];
            for (let i = 0; i < 3; i++) {
                for (let j = 0; j < 3; j++) {
                    const row = blockRow * 3 + i;
                    const col = blockCol * 3 + j;
                    block.push(board[row][col]);
                }
            }
            if (!isValidSet(block)) {
                return false;
            }
        }
    }
    
    return true;
}


console.log("Тест 1 (правильний розв'язок):");
console.log(validSolution([
    [5, 3, 4, 6, 7, 8, 9, 1, 2],
    [6, 7, 2, 1, 9, 5, 3, 4, 8],
    [1, 9, 8, 3, 4, 2, 5, 6, 7],
    [8, 5, 9, 7, 6, 1, 4, 2, 3],
    [4, 2, 6, 8, 5, 3, 7, 9, 1],
    [7, 1, 3, 9, 2, 4, 8, 5, 6],
    [9, 6, 1, 5, 3, 7, 2, 8, 4],
    [2, 8, 7, 4, 1, 9, 6, 3, 5],
    [3, 4, 5, 2, 8, 6, 1, 7, 9]
])); 

console.log("\nТест 2 (неправильний розв'язок з нулями та дублікатами):");
console.log(validSolution([
    [5, 3, 4, 6, 7, 8, 9, 1, 2],
    [6, 7, 2, 1, 9, 0, 3, 4, 8],
    [1, 0, 0, 3, 4, 2, 5, 6, 0],
    [8, 5, 9, 7, 6, 1, 0, 2, 0],
    [4, 2, 6, 8, 5, 3, 7, 9, 1],
    [7, 1, 3, 9, 2, 4, 8, 5, 6],
    [9, 0, 1, 5, 3, 7, 2, 1, 4],
    [2, 8, 7, 4, 1, 9, 6, 3, 5],
    [3, 0, 0, 4, 8, 1, 1, 7, 9]
])); 
