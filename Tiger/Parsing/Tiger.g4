grammar Tiger;

/*
 * Parser Rules
 */

compileUnit
	:	expr EOF																# Program
	;

expr
	:	STRING																	# String
	|	INTEGER																	# Integer
	|	'nil'																	# Nil
	|	lvalue																	# LValue	

	|	'-' expr																# UnaryMinus
	|	expr op=('*' | '/') expr												# Arithmetic
	|	expr op=('+' | '-') expr												# Arithmetic
	|	expr op=('<>' | '=' | '>=' | '<=' | '>' | '<') expr						# Comparison
	|	expr op='&' expr														# Logical
	|	expr op='|' expr														# Logical

	|	lvalue ':=' expr														# Assign
	|	ID '(' (expr (',' expr)*)? ')'											# Call
	|	'(' (expr (';' expr)*)? ')'												# ParenExprs
	|	typeID=ID '{' (ID '=' expr (',' ID '=' expr)*)? '}'						# Record
	|	ID '[' expr ']' 'of' expr												# Array	

	|	'if' expr 'then' expr ('else' expr)?									# If
	|	'while' expr 'do' expr													# While	
	|	'for' ID ':=' expr 'to' expr 'do' expr									# For			
	|	'break'																	# Break
	
	|	'let' decls+ 'in' (expr (';' expr)*)? 'end'								# Let
	;

lvalue
	:	ID																		# IdLValue	
	|	lvalue '.' ID															# FieldLValue
	|	lvalue '[' expr ']'														# IndexLValue
	;

decls
	:	('type' ID '=' type)+													# TypeDecls
	|	'var' id=ID (':' typeId=ID)? ':=' expr									# VarDecl
	|	func_decl+																# FuncDecls
	;

func_decl
	:	'function' id=ID '(' type_fields? ')' (':' typeId=ID)? '=' expr			# FuncDecl
	;

type
	:	ID																		# IdType
	|	'{' type_fields? '}'													# RecordType
	|	'array' 'of' ID															# ArrayType
	;

type_fields
	:	ID ':' ID (',' ID ':' ID)*												# TypeFields
	;

/*
 * Lexer Rules
 */

fragment LETTER		:	[a-zA-Z]												;
fragment DIGIT		:	[0-9]													;

fragment ASCII		:	'0' DIGIT DIGIT
					|	'1' ([0-1] DIGIT | '2' [0-7])
					;

fragment ESCAPE_SEQ	:	'\\' ('n' | 'r' | 't' | '"' | EMPTY* '\\' | ASCII)		; 
fragment CHAR		:	([ -!] | [#-[] | [\]-~]) | ESCAPE_SEQ					;
fragment EMPTY		:	[ \t\n\r]												;

STRING
	:	'"' CHAR* '"'
	;

INTEGER
	:	DIGIT+
	;

ID
	:	LETTER (LETTER | DIGIT | '_')*		
	;

COMMENT
	:	'/*' (COMMENT | .)*? '*/' -> skip
	;

WS	
	:	EMPTY+ -> skip				
	;
