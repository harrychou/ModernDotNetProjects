<Query Kind="Program" />

void Main()
{
	"Harry Chou is here.".Dump();
	AnotherFunction();
}

static void AnotherFunction(){
	"from .... AnotherFunction".Dump();
	// anonymous type
	var obj = new {LastName = "Chou", FristName = "Harry"};
	obj.Dump();
	obj.GetType().Dump();
	
	var person = new Person();
	person.LastName = "Doe";
	person.FirstName = "John";
	person.Dump();
}

class Person {
	public string LastName { get; set; }
	public string FirstName { get; set; }
}
// Define other methods and classes here
