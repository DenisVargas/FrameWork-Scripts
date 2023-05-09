/**
* Este y el modulo "FileUtilities.js" fueron creados con ayuda de ChatGPT
* Es impresionante lo rapido que se puede obtener resultados!
*/

const { ReadTextLinesAsArray, WriteTextArrayToFile } = require('./FileUtilities');

//Usa https://regex101.com/ para crear y testear los regex rapidamente.
const tutorials = /^\[https:\/\/www\.sidefx\.com\/tutorials\/(\S*)]\((\S*)\)/;
const learnCollections = /^\[https:\/\/www\.sidefx\.com\/learn\/collections\/(\S*)]\((\S*)\)/;
const gameDevTuts = /https:\/\/gamedevelopment.tutsplus.com\/tutorials\/(.+)-\d{1,6}/;
const gameDevTutsArticles = /https:\/\/gamedevelopment.tutsplus.com\/articles\/(.+)-\d{1,6}/;

/**
  Replaces links in a file that match a regular expression with a specified format.
  @async
  @function main
  @param {RegExp} regex - The regular expression to match links in the input file.
  @param {Array<Array<string>>} replace - A list of elements to replace in the first group of each matched link.
  Each element is an array of two strings, where the first string is the object to replace and the second is the string to replace it with.
  @returns {Promise<void>}
  @throws {Error} If there is an error reading or writing to the input or output files.
*/
async function main(regex, replace){
  const links = await ReadTextLinesAsArray('./inputs.txt');
  
  let processed = [];
  links.forEach(line => {
    const result = line.replace(regex, (match, group1, group2) => {
      // Reemplazar los elementos especificados en "replace" en el grupo 1
      replace.forEach(([toReplace, replaceWith]) => {
        group1 = group1.replace(new RegExp(toReplace, 'g'), replaceWith);
      });
      
      // Devolver el nuevo string con el formato deseado, con el grupo 2 reemplazado por el match completo
      return `[${group1}](${match})`;
    });
    
    processed.push(result);
  });
  
  await WriteTextArrayToFile('./output.txt', processed);
  console.log("Done");
}

/**
* Replaces Obsidian-style pasted links in a file with markdown-style links.
* 
* @async
* @function cleanObsidianPastedLink
* @param {RegExp} regex - The regular expression to match against each line of the input file. 
*                         The regex should expect to find two capture groups: the first should contain
*                         the name of the link, while the second should contain the link itself.
* @returns {Promise<void>} - A Promise that resolves when the function is finished.
* @throws {Error} - If an error occurs during file reading or writing.
*/
async function cleanObsidianPastedLink(regex) {
  const links = await ReadTextLinesAsArray('./inputs.txt');
  
  let processed = [];
  links.forEach(line => {
    const result = line.replace(regex, (_, group1, group2) => {
      // Reemplazar "-" por " " en el grupo 1
      group1 = group1.replace(/-/g, ' ');
      // Eliminar "/" del grupo 1
      group1 = group1.replace(/\//g, '');
      // Devolver el nuevo string con el formato deseado
      return `[${group1}](${group2})`;
    });
    
    processed.push(result);
  });
  
  await WriteTextArrayToFile('./output.txt', processed);
  console.log("Done");
}


//main(tutorials, [['/'' '],['g', ' ']]);
//main(learnCollections,[['/'' '],['g', ' ']]);
//main(gameDevTuts, [['-',' '],['cms',' ']]);
main(gameDevTutsArticles, [['-',' '],['cms',' ']]);
