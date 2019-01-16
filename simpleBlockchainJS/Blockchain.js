const SHA256 = require("crypto-js/sha256"); //call crypto-js library for sha256

/**
*Block class for each new creation of a block
*/
class Block {

  constructor(index, timestamp, data, previousHash = '') {

    this.index = index;

    this.previousHash = previousHash;

    this.timestamp = timestamp;

    this.data = data;

    this.hash = this.calculateHash();

    this.nonce = 0;

  }

  calculateHash() {
    // take all contents and hash them to get hash of this particular Block
    // JSON.stringify function to turn data of block into string to hash
    return SHA256(this.index + this.previousHash + this.timestamp + JSON.stringify(this.data)+ this.nonce).toString();

  }


  mineBlock(difficulty) {
    while(this.hash.substring(0, difficulty) !== Array(difficulty + 1).join("0")) {
      this.nonce++;
      this.hash = this.calculateHash();

    }
    console.log("block mined:" + this.hash);
  }

}


/**
*BlockChain class
*/
class Blockchain {

  //create genesis Block
  constructor() {
    this.chain = [this.createGenesisBlock()];
    this.difficulty =4 ; // this controls how fast new blocks are added to blockchain
  }

  createGenesisBlock() {
    return new Block(0, "01/14/2018", "Genesis block", "0");
  }


  getLatestBlock() {
    return this.chain[this.chain.length -1]; // genesis block at [0]
  }
  /*Adding New blocks by first making sure prev hash value of new block
  =hash value of prev block*/
  addBlock(newBlock) {
    newBlock.previousHash = this.getLatestBlock().hash;
    newBlock.mineBlock(this.difficulty);
    this.chain.push(newBlock);

  }

  /**Valudating the chain and making sure everythign is stable by checking
  current hash calculation and validating prev block hash to current block's
  prev previousHash
  */
  isChainValid() {
    for (let i = 1; i < this.chain.length; i++) {
      const currentBlock = this.chain[i];
      const previousBlock = this.chain[i -1];
      if (currentBlock.hash !== currentBlock.calculateHash()){
        return false;
      }
      if (currentBlock.previousHash !== previousBlock.hash){
        return false;
      }
      return true;
    }

  }


}

//  Create Blockchain
let AndrewChain = new Blockchain();
// add  blocks
console.log('mining block1');
AndrewChain.addBlock(new Block(1, "01/18/2017", { Data: 5 }));
console.log('mining block2');
AndrewChain.addBlock(new Block(2, "01/18/2017", { Data: 10 }));
console.log('miningblock3');
AndrewChain.addBlock(new Block(3, "01/18/2017", { Data: 25 }));
console.log('miningblock4');
AndrewChain.addBlock(new Block(4, "01/18/2017", { Data: 64 }));

/**
Still incorrect because once you tamper with one block, it affects the
relationship with other blocks since the chain[2] does not have the updated
information obtained from chain[1].
*/
/**
console.log('Is blockchain valid?' + AndrewChain.isChainValid());
AndrewChain.chain[1].data = {data: 1000};
AnrewChain.chain[1].hash = AndrewChain.chain[1].calculateHash();
console.log('Is blockchain valid?' + AndrewChain.isChainValid());
*/

// Blockchain output for own reference
console.log(JSON.stringify(AndrewChain, null, 4));
