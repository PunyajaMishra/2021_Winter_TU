//0xd84fa334b005b22579f6deb4632246aa28d07d9dcd10940b198bfea5f72ed1c4

pragma solidity ^0.5.0;

import "https://github.com/OpenZeppelin/openzeppelin-contracts/blob/v2.3.0/contracts/token/ERC721/ERC721Full.sol";
import "https://github.com/OpenZeppelin/openzeppelin-contracts/blob/v2.3.0/contracts/ownership/Ownable.sol";

contract cert is ERC721Full, Ownable {
   
   using Counters for Counters.Counter;
   Counters.Counter private tokenId;
   
   //declare the struct fpr Certificate with all its variables
   struct certificate{
       address student; //global variable to store student's address
       address issuer; //global variable to store issuer's address
       string studentName; //student Name
       string certificateName; //Certificate Name - subject or course
       string cert_hash;  //transaction hash for certificate
       uint16 dateIssued; //the date the certificate is issued on
   }
    
    certificate[] public students;

 constructor(
    string memory name,
    string memory symbol
  )
    ERC721()
    public
  {}
function viewcert( uint256 certTokenId ) public view returns(string memory studentName, address student, address issuer, string memory certificateName, uint16 dateIssued, string memory cert_hash) {
    certificate memory _certificate = students[certTokenId];
    studentName = _certificate.studentName;
	student = _certificate.student;
	issuer = _certificate.issuer;
    certificateName = _certificate.certificateName;
	dateIssued = _certificate.dateIssued;
	cert_hash = _certificate.cert_hash;
	return (studentName, student, issuer, certificateName, dateIssued, cert_hash);
     }


function mint(string memory _studentName, address _student,address _issuer, string memory _certificateName, uint16 _dateIssued, string memory _cert_hash) public payable onlyOwner {
   uint256 certTokenId = tokenId.current();
   certificate memory _certificate = certificate({studentName: _studentName, student: _student, issuer: _issuer, certificateName: _certificateName, dateIssued: _dateIssued, cert_hash: _cert_hash});
   students.push(_certificate);
  _mint(msg.sender, certTokenId);
   tokenId.increment();
 }

        
}