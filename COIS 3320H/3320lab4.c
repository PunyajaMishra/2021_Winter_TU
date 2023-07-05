//program creates files as structs //which are pkaed in directory
//struct dir : char name[50] char ID[10]
//struct file : char name[50] char ID[10]

//root directory - head - first node
//next - doubly linked list - first file

//2 files and 1 folder

//Alaadin Addas
//Yes the next for a file will always be null, there are two ways around this that I mentioned in the lab. 
//1- When inserting into the doubly linked list make a method that inserts based on the parent 
//(so you insert the node and next is null while the parent is previous). 
//2- Make the files have a next point that is not null but points to the next file entered. 

//I used method 2. 

#include<stdio.h>
#include<stdlib.h>
#include<string.h>

//the directory struct
struct dir{
    char name[50]; 
    //char ID[10];
};

//the file struct
struct file{
    char name[50]; 
    //char ID[10];
};

//Node struct - doubly linked list is made of struct Node*
struct Node{
    int ID; //to know if it user wants to create a new file or folders
    struct file* File; //the file pointer
    struct dir* directory; //the dir pointer

    //pointers to next and prev
    struct Node* next;
    struct Node* prev;
};

//pointer to point at head of the linked lis
struct Node* head; 
//GetNEwNode is the method that returns node and initializes the node
struct Node* GetNewNode(struct dir *folder, struct file *File, int ID); 
//Insert is th emethod to insert the node at the end
void Insert(struct dir *folder, struct file *File, int ID);

//method to print the doubly linked list
void print(){
    struct Node* temp = head; //temp node to point to head/root
    printf("\nThe doubly linked list : ");
    //a loop to keep going till the end of the loop
    while(temp!=NULL){
        //prnt the folder name if it isis a directory
        if(temp->ID == 0) printf("\n%s", temp->directory->name);
        //print th efile name if it is a file
        else if(temp->ID == 1) printf("\n%s", temp->File->name);
        temp = temp-> next;
    }
}

//Main function
int main(){

    head = NULL; //initialize to null

    //create the root - the file, directory, prev, next all are null
    //struct Node* root = GetNewNode(NULL, NULL, 2);
    //head = root;
    
    //ID = 0 for directory and 1 for file

    //the first "directory" - root
    struct dir* root = (struct dir*)malloc(sizeof(struct dir));
    strcpy(root->name, "root");
    //first file
    struct file* file1 = (struct file*)malloc(sizeof(struct file));
    strcpy(file1->name, "alaadin.pdf");
    //second file
    struct file* file2 = (struct file*)malloc(sizeof(struct file));
    strcpy(file2->name, "punyaja.docx");
    //a folder - src
    struct dir* dir1 = (struct dir*)malloc(sizeof(struct dir));
    strcpy(dir1->name, "src");
    //file under src
    struct file* file3 = (struct file*)malloc(sizeof(struct file));
    strcpy(file3->name, "0660001.pdf");
    
    //inserting them all in order
    Insert(root, NULL, 0);
    Insert(NULL, file1, 1);
    Insert(NULL, file2, 1);
    Insert(dir1, NULL, 0);
    Insert(NULL, file3, 1);

    // print the doubly linked list
    print();

    return 0;
}

//the method get new node takes a directory struct, file struct and th eint identifying that
struct Node* GetNewNode(struct dir *folder, struct file *File, int ID){
    struct Node* newNode = (struct Node*)malloc(sizeof(struct Node));
    //id ==0 means it is a directory
    if(ID == 0){
        newNode->directory = folder;   
    }
    //id == 1 means it is a file
    else if(ID == 1){
        newNode->File = File;
    }
    //setting all other values
    newNode->ID = ID;
    newNode->prev = NULL;
    newNode->next = NULL;
    return newNode;
}

//inserting the node on th elinked list
void Insert(struct dir *folder, struct file *File, int ID){
    struct Node* temp = head; //making a temp node
    //initilaizing the new node
    struct Node* newNode = GetNewNode(folder, File, ID);
    //if head is nukk, initializing the first node
    if(head == NULL){
        head = newNode;
        return;
    }
    
    while(temp->next!=NULL) temp = temp->next;
    temp->next = newNode;
    newNode->prev = temp;
}
