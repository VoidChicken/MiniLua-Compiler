#define string String^

using namespace System;
using namespace System::IO;
using namespace System::Collections::Generic;
using namespace System::Diagnostics;
namespace Assembler {
	public ref class Converter {
	public:
		List<String^>^ lines = gcnew List<String^>();
		void processLine(string line) {
			lines->Add(line);
		}
		void finish(string dest) {
			
			StreamWriter^ sw = File::CreateText(dest + "_out.c");
			for (int i = 0; i < lines->Count; i++) {
				sw->WriteLine(lines->ToArray()[i]);
			}
			sw->Close();
			//Process::Start("cl " + dest + ".c");
		}
	};
}