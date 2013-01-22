var G_vmlCanvasManager;
// Canvas
var ctx;
// Width of field
var Width;
// Height of field
var Height;
// Array of cell
var Array;
// Cell size
var CellSize = 50;

// Begin draw
function startCanvas(model) {
    var tempModel = $.parseJSON(model);
    Width = tempModel.WidthField;
    Height = tempModel.HeightField;
    Array = tempModel.Array;
    CellSize = 500 / Width;
    var canvas = document.getElementById('canvas');

    // Проверяем для IE
    if (G_vmlCanvasManager != undefined)
        G_vmlCanvasManager.initElement(canvas);

    // Проверяем понимает ли браузер canvas
    if (canvas.getContext) {
        ctx = canvas.getContext('2d'); // Получаем 2D контекст
    }
    drawField(Width, Height);
    drawModel();
    UpdateData();
}
// Draw all cell for them status
function drawModel() {
    for (var i = 0; i < Width * Height; i++) {
        switch (Array[i].Status) {
        case 0:
            drawCell(Array[i].X, Array[i].Y, 'Empty');
            break;
        case 1:
            drawCell(Array[i].X, Array[i].Y, 'Born');
            break;
        case 2:
            drawCell(Array[i].X, Array[i].Y, 'Live');
            break;
        case 3:
            drawCell(Array[i].X, Array[i].Y, 'Dead');
            break;
        }
    }
}
// Update data
function UpdateData() {
    setInterval(function () {
        $.post("/Main/UpdateGameModel", {}, function (data) {
            Array = data.Array;
            drawModel();
        });
    }, 500);
}
// Draw field of cell
function drawField(width, height) {
    Width = width;
    Height = height;
    
    ctx.lineWidth = 1; // Ширина линии
   // ctx.fillStyle = '#00FF00'; // Цвет заливки
   // ctx.strokeStyle = '#FF0000'; // Цвет обводки

    ctx.strokeRect(0, 0, CellSize * Width, CellSize * Height);

    for (var i = 0; i < Width; i++) {
        ctx.moveTo(0, CellSize * i); // Начало линии 
        ctx.lineTo(CellSize * Width, CellSize * i); // Узел линии  
        ctx.stroke();
    }

    for (var i = 0; i < Height; i++) {
        ctx.moveTo(CellSize * i, 0); // Начало линии 
        ctx.lineTo(CellSize * i, CellSize * Height); // Узел линии  
        ctx.stroke();
    }
}
// Draw one cell with special color
function drawCell(x, y, type) {
    switch (type) {
    case 'Empty':
        ctx.fillStyle = '#FFFFFF';
        break;
    case 'Born':
        ctx.fillStyle = '#FFFF00';
        break;
    case 'Live':
        ctx.fillStyle = '#00FF00';
        break;
    case 'Dead':
        ctx.fillStyle = '#FF0000';
        break;
    }
ctx.fillRect(CellSize * x + 1, CellSize * y + 1, CellSize - 2, CellSize - 2);
}

