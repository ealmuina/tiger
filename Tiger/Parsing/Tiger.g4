grammar Tiger;

/*
 * Parser Rules
 */

compileUnit
	:	expr EOF
	;

expr
	:	STRING															# String
	|	INTEGER															# Integer
	|	'nil'															# Nil
	|	lvalue															# LValue	

	|	'-' expr														# UnaryMinus
	|	e1=expr op=('*' | '/') e2=expr									# Arithmetic
	|	e1=expr op=('+' | '-') e2=expr									# Arithmetic
	|	e1=expr op=('<>' | '=' | '>=' | '<=' | '>' | '<') e2=expr		# Comparison
	|	e1=expr op=('&' | '|') e2=expr									# Logical
	
	|	lvalue ':=' expr												# Assign
	|	ID '(' (expr (',' expr)*)? ')'									# Call
	|	'(' (expr (';' expr)*)? ')'										# ParenExprs
	|	typeID=ID	'{' (ID '=' expr (',' ID '=' expr)*)? '}'			# Record
	|	ID '[' e1=expr ']' 'of' e2=expr									# Array	

	|	'if' e1=expr 'then' e2=expr ('else' e3=expr)?					# If
	|	'while' e1=expr 'do' e2=expr									# While	
	|	'for' ID ':=' e1=expr 'to' e2=expr 'do' e3=expr					# For			
	|	'break'															# Break
	
	|	'let' decl* 'in' (expr (';' expr)*)? 'end'						# Let
	;

lvalue
	:	ID																# IdLValue	
	|	lvalue '.' ID													# FieldLValue
	|	lvalue '[' expr ']'												# IndexLValue
	;

decl
	:	'type' ID '=' type												# TypeDecl
	|	'var' ID (':' ID)? ':=' expr									# VarDecl
	|	'function' ID '(' type_fields? ')' (':' ID)? '=' expr			# FuncDecl
	;

type
	:	ID																# IdType
	|	'{' type_fields? '}'											# RecordType
	|	'array' 'of' ID													# ArrayType
	;

type_fields
	:	ID ':' ID (',' ID ':' ID)*
	;

/*
 * Lexer Rules
 */

fragment LETTER		:	[a-zA-Z]								;
fragment DIGIT		:	[0-9]									;
fragment ESCAPE_SEQ	:	'\\' ('n' | 'r' | 't' | '"' | '\\')		; // TODO falta el \ddd
fragment CHAR		:	[ -~] | ESCAPE_SEQ						;
fragment EMPTY		:	[ \t\n\r]								;

COMMENT
	:	'/*' (COMMENT | .)*? '*/' -> skip	
	;

STRING
	:	'"' CHAR* ('\\' EMPTY* '\\' CHAR*)? '"'	
	;

INTEGER
	:	DIGIT+
	;

ID
	:	LETTER (LETTER | DIGIT | '_')*		
	;

WS	
	:	EMPTY+ -> skip				
	;
