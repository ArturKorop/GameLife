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
// Canvas width
var CanvasWidth = 500;
// Canvas height
var CanvasHeight = 500;
// Model age
var Age = 0;

// Begin draw
function startCanvas(model) {
    alert(model);
    CanvasWidth = $('#canvas').prop('width');
    CanvasHeight = $('#canvas').prop('height');
    var tempModel = $.parseJSON(model);
    Width = tempModel.WidthField;
    Height = tempModel.HeightField;
    Array = tempModel.Array;
    Age = tempModel.Age;

    if ((CanvasWidth / Width) > (CanvasHeight / Height)) {
        CellSize = CanvasHeight / Height;
    } else {
        CellSize = CanvasWidth / Width;
    }
    
    var canvas = document.getElementById('canvas');

    // Chech for IE
    if (G_vmlCanvasManager != undefined)
        G_vmlCanvasManager.initElement(canvas);

    // Check if browser can support canvas
    if (canvas.getContext) {
        ctx = canvas.getContext('2d'); // Получаем 2D контекст
    }
    drawField(Width, Height);
    drawModel();
    UpdateData();
}
// Draw all cell for them status
function drawModel() {
    $('#Age').text(Age);
    for (var i = 0; i < Width * Height; i++) {
        switch (Array[i].Status) {
        case 0:
            drawCell(Array[i].X, Array[i].Y, 'Empty', null);
            break;
        case 1:
            drawCell(Array[i].X, Array[i].Y, 'Born', Array[i].Organism.Genome);
            break;
        case 2:
            drawCell(Array[i].X, Array[i].Y, 'Live', Array[i].Organism.Genome);
            break;
        case 3:
            drawCell(Array[i].X, Array[i].Y, 'Dead', null);
            break;
        case 4:
            drawCell(Array[i].X, Array[i].Y, 'Create', Array[i].Organism.Genome);
            break;
        }
    }
}
// Update data
function UpdateData() {
    var updateInterval = setInterval(function () {
        prepare();
        var b2 = setTimeout(step(), 1000);
    }, 2000);
}
// Step of life
function step() {
    $.post("/Main/UpdateGameModel", {}, function(data) {
        Array = data.Array;
        drawModel();
        $('#Age').text(data.Age);
    });
}
// Prepare to step
function prepare() {
    $.post("/Main/PreapreGameModel", {}, function (data) {
        Array = data.Array;
        drawModel();
        $('#Age').text(data.Age + ' prepare');
    });
}
// Draw field of cell
function drawField(width, height) {
    Width = width;
    Height = height;
    
    ctx.lineWidth = 1; // Line widht

    ctx.strokeRect(0, 0, CellSize * Width, CellSize * Height);
    var i;
    for (i = 0; i < Width; i++) {
        ctx.moveTo(CellSize * i, 0); // Begin line
        ctx.lineTo(CellSize * i, CellSize * Height);
        ctx.stroke(); // End line
    }

    for (i = 0; i < Height; i++) {
        ctx.moveTo(0, CellSize * i); // Begin line
        ctx.lineTo(CellSize * Width, CellSize * i);
        ctx.stroke(); // End line
    }
}
// Draw one cell with special color

function drawCell(x, y, type, genome) {
    switch (type) {
    case 'Empty':
        ctx.fillStyle = '#FFFFFF';
        break;
    case 'Dead':
        ctx.fillStyle = '#FFFFFF';
        break;
    /*  case 'Born':
        ctx.fillStyle = '#FFFF00';
        break;
        case 'Live':
        ctx.fillStyle = '#00FF00';
        break;
        case 'Create':
        ctx.fillStyle = '#FFFF00';
        break;*/
    }
if (type == 'Born' || type == 'Live' || type == 'Create') {
    //alert(genome.toString(2));
    if (genome < 2) {
        ctx.fillStyle = '#FF0000';
    }
    else if (genome < 4) {
        ctx.fillStyle = '#FF3300';
    }
    else if (genome < 8) {
        ctx.fillStyle = '#00CC00';
    }
    else if (genome < 16) {
        ctx.fillStyle = '#33FF00';
    }
    else if (genome < 32) {
        ctx.fillStyle = '#FF6600';
    }
    else if (genome < 64) {
        ctx.fillStyle = '#FFFF00';
    }
    else if (genome < 128) {
        ctx.fillStyle = '#B580FE';
    }
    else if (genome < 256) {
        ctx.fillStyle = '#009999';
    }
    else {
        ctx.fillStyle = '#0033CC';
    }
}

    ctx.fillRect(CellSize * x + 1, CellSize * y + 1, CellSize - 2, CellSize - 2);
}

