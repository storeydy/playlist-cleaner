const fs = require('fs');
const path = require('path');
const dotenv = require('dotenv');

dotenv.config();

console.log("making it here");

const envFiles = [
    path.join(__dirname, 'environment.ts'),
    path.join(__dirname, 'environment.development.ts'),
    path.join(__dirname, 'environment.production.ts')
];

envFiles.forEach((filePath) => {
    let envFileContent = fs.readFileSync(filePath, 'utf-8');

    envFileContent = envFileContent.replace('${CLIENT_ID}', process.env.CLIENT_ID);

    fs.writeFileSync(filePath, envFileContent);
});