const fs = require('fs');
const { syncBuiltinESMExports } = require('module');
const readline = require('readline');

async function ReadTextLinesAsArray(filePath) {
  const fileStream = fs.createReadStream(filePath);
  const rl = readline.createInterface({
    input: fileStream,
    crlfDelay: Infinity
  });

  const lines = [];

  for await (const line of rl) {
    // Si la línea no está vacía, la agregamos al array
    if (line.trim() !== '') {
      lines.push(line);
    }
  }

  return lines;
}

async function WriteTextArrayToFile(filePath, lines) {
  const data = lines.join('\n');

  try {
    await fs.promises.writeFile(filePath, data);
    console.log(`El archivo ${filePath} ha sido escrito.`);
  } catch (err) {
    console.error(`Error al escribir el archivo: ${err}`);
  }
}

module.exports = { ReadTextLinesAsArray, WriteTextArrayToFile };
