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
	|	e1=expr op=('*' | '/') e2=expr									# MulDiv
	|	e1=expr op=('+' | '-') e2=expr									# AddSub
	|	e1=expr op=('<>' | '=' | '>=' | '<=' | '>' | '<') e2=expr		# Comp
	|	e1=expr op=('&' | '|') e2=expr									# Logical
	|	lvalue ':=' expr												# Assign
	|	ID '(' expr_list? ')'											# Call
	|	'(' expr_seq? ')'												# ParenExprs
	|	ID	'{' field_list? '}'											# Record
	|	ID '[' e1=expr ']' 'of' e2=expr									# Array	
	|	'if' e1=expr 'then' e2=expr ('else' e3=expr)?					# If
	|	'while' e1=expr 'do' e2=expr									# While	
	|	'for' ID ':=' e1=expr 'to' e2=expr 'to' e3=expr					# For			
	|	'break'															# Break
	|	'let' decl* 'in' expr_seq? 'end'								# Let
	;

expr_seq
	:	expr (';' expr)*
	;

expr_list
	:	expr (',' expr)*
	;

field_list
	:	ID '=' expr (',' ID '=' expr)*
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
