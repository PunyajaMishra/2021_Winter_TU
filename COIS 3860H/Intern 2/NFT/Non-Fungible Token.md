# Non-Fungible Token

We are going to learn today what are Non-fungible Tokens (NFTs) , what are Fungible Tokens, what is ERC-721 and REC-20 tokens. We will learn how to build NFTs and deploy them. No pre-requisite is required for this content and let's jump into it. 



## What are NFTs?

NFTs or Non-Fungible Tokens are tools used on Blockchain world. You would not really find NFTs outside of Blockchain space. NFTs allows for range of possibilities and applications. NFTs can be anything - a cat, a dog, a color token, anything that you want. THe difference between NFT and fungible Tokens is that every Fungible Token has the same value - like a dollar is well, a dollar. And hence people can exchange two fungible tokens without worrying about the value as long as the number of tokens exchanged were fine. NFTs on the other hand do not have same value. Let's say if you made dogs as your NFTs, then a great dane might have more value than a golden retriever. Trading NFTs is not easy because one has to ensure that the value remains the same. Every NFT has a unique id which decides its worth. Anothier example of NFT would be baseball carss - not every baseball cars have the same worth, some are mint condition and some can be foun in every store. One example of a fungible token is OMG cryptocurrency - OMG is an ERC-20 token so every OMG can be traded with another OMG token of same value because every OMG token has the same worth as any other OMG. 

Then What are ERC 721 and ERC 20 tokens? ERC-721 Token is an example of NFT and ERC-20 token is an example of fungible token. ERC-721 standard is basicalllly just a specification that says how the smart contract is supposed to work. It describes the type of functions that the smart contract implements like what the arguments for these functions are and the return values.  



That is an overview of what NFTs, fungible tokens, ERC-721, ERC-20 are. Now let's build our own NFTs. 



https://www.youtube.com/watch?v=YPbgjPPC1d0



## Dependencies Required for building NFTs

1. We need to have **Node.js** already installed. To check if you already have Node.js installed, simple go to the command terminal and type node.js. If it does not show any error, then you already have node.js installed. If it shows errors, then you can install it by going to the [website](https://nodejs.org/en/download/).

   Or go to terminal and type ->  *npm install node.js* ->This is also an easy way. 

   

2. We will be using Ganche as our personal blockchain. You can download Ganache from [this link](https://truffleframework.com/ganache). Once downloaded, installed and launched, you should be able to see two options -  "Quickstart" and "New Workspace". Click on the first option, Quickstart - and you will see 10 test accounts with 100 ethers in each of them. We will be using these public addresses for our project - these are test ethers and that is all. 

   What is a personal blockchain and why will be using Ganache?  A personal blockchain is like a real blockchain and anyone in public can run it but it runs on our own local computer on a closed Network. Ganache is basically a process that runs on a computer that spins up this blockchain and runs on a server so we can use this to develop smart contracts that run tests against it and we can run scripts against the network develop applications and actually talk to this blockchain. 

   

3.  Another tool we will be needing is Truffle. You can get truffle by running the command - npm install -g truffle@5.0 on the terminal. 

   We need truffle framework to develop Ethereum smart contracts with the solidity programming language. It is important to use the exact version of the truffle as mentioned above because we will be writing our contracts in 0.5.0 versions. 

   

4. Metamask - Metamask, is a digital wallet that connects with blockchain network and has test networks where ewe can run our smart contracts. If you still don't have your metamask account then you should install it from going to google store and clicking install and then add it as an extension. 

   

5. Finally, we are using a GitHub repository project because building it from scratch will take a lot of time. We will also be using open zeppelin libraries. Open Zeppelin has designed this go to codes for tokens and ownership and many different blockchain tools. So, we import them in our ocde and use them to make our project easier and smooth to run. You need to clone the repository into your local computer. The repository contains truffle-config files which will automatically connect to ganache and run easily. 

   To clone, go to terminal and write : 

   git clone https://github.com/PunyajaMish/nft NFTs

   We have named the folder NFTs. Make sure every library being downloaded is in the same folder in your computer so that the code is able to find it when you are importing. 

   Next, also download the open zeppelin library. [This link](https://github.com/OpenZeppelin/openzeppelin-contracts) is the link to their repository. And to install go to terminal (in the same directory where you have been) and type - 

   npm install @openzeppelin/contracts 

   You will also find this in the README file. README files are a great place to look at if you get stuck because they have information about the project in detail for people to understand and work with it. 

   

6. Text editor : VS Code. We need a text editor where we can code our smart contract. For this project, we are using VS Code. You can also use Sublime Text if you like. 



## Let's start

1. Open the text editor and open the folder NFTs. Go to File > Open folder and then go to te directory where you have been installing everything.

   

2. Make surge Ganache is running on localhost for it to be connected to truffle so that we are talking to blockchain. 

   

3. Now let's look at the smart contract : Color.sol

   We are using ERC721 token file's help from open zeppelin because it has the standard code and all functionalities for an NFT to run.

   

4. I have also used truffle-flattener. What is truffle flattener? It is a tool that makes sure that the code we are using from open zeppelin remains the same. Open Zeppelin keeps updating their code and after a few weeks you might see that the compiler version has changed to 0.8.6 or the files we were using are not there anymore. For example, we are using ERC721Full.sol because it also has link to ERCMetadata, ERC721Enumerable and ERC165. By using truffle flattener, all the files are compressed and added into the same file ERCFull.sol which we have added in out src > contracts for using. It has already been done but to do so first this is how we do : type this in terminal : 

   ./node_modules/.bin/truffle-flattener file to be flattened path > where to be flattened folder path and name

   

   ### Smart contract

   The NFT we are building is Colors - as the name suggests. Let's understand the smart contract we have : 

   **contract Color is ERC721Full** : This means that our contract will be using ERC721Full.sol 

   I have added comments to the code to understand each line of the code. 

   ![constructor](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\begining contract.png)

   

Now let's look at the code. We start with constructor. ERC721 has a constructor that takes 2 parameters, so we need to pass 2 paramteters as well.  The first parameter -  name will be "Color" because that is what we are making and symbol will be "COLOR". We also have to set the visibility of the function so all the functions of smart contracts have visibility there either public or private. So we will make the constructor public. 

![minting token function](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\minting token.png)

### Tests Folder

In order to check if the code is working and connecting to blockchain, we need a test.js file which is in the test folder. The test was written using *Mocha Framework* - ([website](https://mochajs.org/)). This helps write tests for the smart contracts and also the chai assertion library and this allows us to write successful test examples. 

First we import the file smart contract by - **const Color = artifacts.require('./Color.sol')**

We also have abis folders which is the abstract binary interface of the file - basically a JSON description of what the smart contract looks like, the functions, information like what the address wants to put on the network and so on. 

First we import chai - **require('chai')**

 **.use(require('chai-as-promised'))**

 **.should()**

#### Describe Deployment

![describe deployment](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\describe deployment.png)

#### Describe Minting

The function mint we are using in our smart contract is from ERC721Full and it uses the function ***transfer*** also described in ERC721 - so basically it tells whom the token was minted to and the token id as well. 

![describing minting](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\decribing minting.png)



#### Describing Indexing

If we want to see all the colors that exist, we need this part of the code

![describing indexing](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\describing indexing.png)

### Migrations folder

We need migrations file like the initial_migration file to put the smart contracts on the blockchain and this is just like migrating a database in some other context where you're adding new tables or maybe you're moving data from one database to another. That's kind of the idea with smart contract migrations - you're moving some our contracts one place to another, you're migrating them or you're changing the state from one state to another. 

So, we create another migrations file names 2_deploy_contratcs.js : 2 is for the compiler to know that it should be ran second. We have the same code as the initial_migration, except we just change the Migrations to Color to tell that Color.sol smart contract should run. 

To finally migrate it we go to terminal and type - 

***truffle migrate***

If it does not show any errors that means it was migrated successfully. 

Next we type in 

***truffle console***

First we write the smart contracts in solidity, then we interact with JavaScript to test them. JavaScript has the actual functions that we need to ensue the smart contract is running. We do this iin console to have lightweight runtime environment. To get the smart contract we type in : 

***Color.deployed()***

This will get a deployed copy of the smart contract and return it. Now since we have *async await* method, therefore we need to type in : 

***color=await* *Color.deployed()***

This is what we have in our test file as well. 



## Client-side portion of the Application

Now we will look into the Components folder - App.js and App.css. Mostly App.js because it has all the functionalities. App.css is just how the designing needs to be. App.js is written in React.js. React implements ***render()*** method that takes input data and returns what to display. The syntax type is XML-like syntax called JSX. Input data passed into component can be accessed by ***render()*** via ***this.props***. In short we mix and match HTML and JS code. Check out the React.org website to know more about React if you do not know. 

We can just run react project by typing in terminal  (new window) : 

***npm run start***

Then we type ***Yes*** and we get the starter kit of the DApp University since they have one that wires up with React and truffle and ganache. This allows us to avoid writing to much design code - CSS. 

#### Required import - Web3

We are using React.js, App.css and Color.json file, but we need one more important thing and that is ***Web3***. Web3 is a JS library that basically converts our web browser into a blockchain. on the web3.js site, we can get instance of web3.js - it gives an Ethereum provider. Out Ethereum provider is Metamask in this case. Make sure we are running on localhost port 7545. 

![web3](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\web3.png)



We need two functions - one that will connect to the Web3 and one that will connect and fetch the smart contract finally to run. Go to [this link about Web3](https://web3js.readthedocs.io/en/v1.3.4/) and you will find all the functions we have used in here.

![connect browser to Web3](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\loadweb3.png)



 #### Fetch our smart contract

After we are connected with Web3, we still need to fetch the smart contract to connect it to the web browser of the client-side. We do this by using network id, ABI of the contract and web3. 

![load contract](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\loadsmartcontract.png)



#### Load the NFT tokens onto the array

Once we are all connected, we should be able to access and load the NFT tokens onto an array so if we want to print them off, we should be able to do so. 

We also need a default constructor that sets the contract as null and total supply as 0 - basically the default values of everything at the beginning when no NFTs have been minted. 

![load tokens](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\loadcolors.png)

#### React

React, as mentioned, i a mixture of HTML and JS. As soon as ***render()*** is called, we are running react.js. In this part of the code we are building the navigation bar where we print the header - ***Color Tokens*** because that is what our NFT is and then we  also print the address of the owner who is trying to mind the token. This way we can see what account is being used. 

![React](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\react.png)

![print nfts](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\print nfts.png)





## Running the program finally

Now, that we are all set - let us run the contract on blockchain. There are a few steps we need to follow even here in order to ensure that it is connected and running properly. First of all we need to run the React and make sure that a new tab opens on the browser with the output we desire. 

#### 	React

1. To render(), we need to ensure we have react installed. Open Terminal, and go the same directory where everything is. Since, we previously did install node_modules, and the repository that you cloned also has node_modules, there will be react. To check simple type in - ***npm run start***

   If it shows error then that means, you do not have react or are in the wrong folder(not where we have our entire project in). Else simply run ***npm install --save react***.

   #### Metamask Network

2. Next, once ***npm run start*** command runs,  a new tab will load on to your web browser. Now we need to ensure that our Metamask is on the right network and also we are on the right account. 

   The network we are in is **Host 127.0.0.1 : Port 7545**.

   To change the network click on the dropdown at the top of the metamask extension pop up. 

   ![network](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\network.png)

   There is a possibility that you might not have the option of this network. So, we can add the network. Click on ***Custom RPC*** - which is at the very end of the list above. You should see : 

   ![custom rpc](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\custom rpc.png)

   

   How to know what the host name and rpc url is? Open Ganache and you will find that at the top bar - it has all the information you need

   ![Ganache](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\ganache.png)

   Network Name : (you can give whatever you want. I have given - ) Host//127.0.0.1:7545

   New RPC URL : HTPP://127.0.0.1:7545

   Chain ID : 1337 (if not, once you hit Save, it will show an error and tell you the right Chain ID).

   #### Account

   3. Next, we need to ensure we are using one of the test accounts in the Ganache because it has test ethers on there. Click on the small circle that is on the top right corner in Metamask and then hit ***import account***

      ![import account](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\import account.png)

      It will ask you for the private key. Open Ganache, choose any account you want, and hit the key at the very right - that is your private key. These are test accounts so it does not matter, but when you are using the real account, never show you private key to anyone if you do not wish to lose any funds. Enter the Private key and you are good to go! Select that account - as you can see above, i have chosen Account 3 because that is the test account I have to use. 

      #### Minting NFTs

   4. Finally, now we can mint the NFT tokens. You should be able to see your tab like this : 

      ![issue token](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\issue token.png)

   

   We have our header ***Color Tokens *** and the account we are using to mint on the navigation bar. 

   Go to [Color Hunt website](https://colorhunt.co/) and find yourself your favorite color and copy the code of that color. Come back to the Color Tokens page and enter the code with ***#*** > And hit Mint. 

   Let's say I am minting the color ***#A239EA*** Once I hit mint, my metamask should pop up with information like this : This is just showing me the gas fee required and asking me to confirm the transaction. Hit confirm and reload the page. 

   ![Mint confirm](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\mint.png)

   Once you reload, you should be able to see the new NFT token color code, the color and also in your metamask account - ethers will have been deducted. 

   ![NFTs](C:\Users\punya\Documents\Priyam\COIS 3860H\Intern 2\NFT\NFTs.png)

As you can see, I have minted 4 NFTs until now and Ethers have also been deducted form my account and are no longer 100 Ethers. 



And that is it! You have learned how to code, build and deploy your own NFTs!