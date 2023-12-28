◊
e/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Configurations/JwtConfig.cs
	namespace 	
WebShopRestService
 
. 
Configurations +
{ 
public 

class 
	JwtConfig 
{ 
public 
string 
Secret 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} ¨
m/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Configurations/MapperInitializer.cs
	namespace 	
WebShopRestService
 
. 
Configurations +
{ 
public 

class 
MapperInitializer "
:# $
Profile% ,
{ 
public		 
MapperInitializer		  
(		  !
)		! "
{

 	
	CreateMap 
< 
Product 
, 

ProductDTO )
>) *
(* +
)+ ,
., -

ReverseMap- 7
(7 8
)8 9
;9 :
	CreateMap 
< 
Product 
, 
CreateProductDTO /
>/ 0
(0 1
)1 2
.2 3

ReverseMap3 =
(= >
)> ?
;? @
} 	
} 
} ´2
l/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Controllers/AddressesController.cs
	namespace 	
WebShopRestService
 
. 
Controllers (
{		 
[

 
Route

 

(


 
$str

 
)

 
]

 
[ 
ApiController 
] 
[ 
	Authorize 
] 
public 

class 
AddressesController $
:% &
ControllerBase' 5
{ 
private 
readonly 
AddressesManager )
_addressesManager* ;
;; <
public 
AddressesController "
(" #
AddressesManager# 3
addressesManager4 D
)D E
{ 	
_addressesManager 
= 
addressesManager  0
;0 1
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
ActionResult &
<& '
IEnumerable' 2
<2 3
Address3 :
>: ;
>; <
>< =
GetAddresses> J
(J K
)K L
{ 	
var 
	addresses 
= 
await !
_addressesManager" 3
.3 4
GetAddressesAsync4 E
(E F
)F G
;G H
if 
( 
	addresses 
== 
null !
)! "
{ 
return 
NotFound 
(  
$str  8
)8 9
;9 :
} 
return"" 
Ok"" 
("" 
	addresses"" 
)""  
;""  !
}## 	
[&& 	
HttpGet&&	 
(&& 
$str&& 
)&& 
]&& 
public'' 
async'' 
Task'' 
<'' 
ActionResult'' &
<''& '
Address''' .
>''. /
>''/ 0

GetAddress''1 ;
(''; <
int''< ?
id''@ B
)''B C
{(( 	
var)) 
address)) 
=)) 
await)) 
_addressesManager))  1
.))1 2
GetAddressByIdAsync))2 E
())E F
id))F H
)))H I
;))I J
if++ 
(++ 
address++ 
==++ 
null++ 
)++  
{,, 
return-- 
NotFound-- 
(--  
$"--  "
$str--" 2
{--2 3
id--3 5
}--5 6
$str--6 A
"--A B
)--B C
;--C D
}.. 
return00 
Ok00 
(00 
address00 
)00 
;00 
}11 	
[44 	
HttpPut44	 
(44 
$str44 
)44 
]44 
[55 	
	Authorize55	 
(55 
Roles55 
=55 
$str55 *
)55* +
]55+ ,
public66 
async66 
Task66 
<66 
IActionResult66 '
>66' (

PutAddress66) 3
(663 4
int664 7
id668 :
,66: ;
Address66< C
address66D K
)66K L
{77 	
if88 
(88 
id88 
!=88 
address88 
.88 
	AddressId88 '
)88' (
{99 
return:: 

BadRequest:: !
(::! "
$str::" 8
)::8 9
;::9 :
};; 
try== 
{>> 
await?? 
_addressesManager?? '
.??' (
UpdateAddressAsync??( :
(??: ;
address??; B
)??B C
;??C D
}@@ 
catchAA 
(AA  
KeyNotFoundExceptionAA '
)AA' (
{BB 
returnCC 
NotFoundCC 
(CC  
$"CC  "
$strCC" 2
{CC2 3
idCC3 5
}CC5 6
$strCC6 A
"CCA B
)CCB C
;CCC D
}DD 
returnFF 
	NoContentFF 
(FF 
)FF 
;FF 
}GG 	
[JJ 	
HttpPostJJ	 
]JJ 
[KK 	
	AuthorizeKK	 
(KK 
RolesKK 
=KK 
$strKK *
)KK* +
]KK+ ,
publicLL 
asyncLL 
TaskLL 
<LL 
ActionResultLL &
<LL& '
AddressLL' .
>LL. /
>LL/ 0
PostAddressLL1 <
(LL< =
AddressLL= D
addressLLE L
)LLL M
{MM 	
varNN 
createdAddressNN 
=NN  
awaitNN! &
_addressesManagerNN' 8
.NN8 9
CreateAddressAsyncNN9 K
(NNK L
addressNNL S
)NNS T
;NNT U
returnPP 
CreatedAtActionPP "
(PP" #
nameofPP# )
(PP) *

GetAddressPP* 4
)PP4 5
,PP5 6
newPP7 :
{PP; <
idPP= ?
=PP@ A
createdAddressPPB P
.PPP Q
	AddressIdPPQ Z
}PP[ \
,PP\ ]
createdAddressPP^ l
)PPl m
;PPm n
}QQ 	
[TT 	

HttpDeleteTT	 
(TT 
$strTT 
)TT 
]TT 
[UU 	
	AuthorizeUU	 
(UU 
RolesUU 
=UU 
$strUU *
)UU* +
]UU+ ,
publicVV 
asyncVV 
TaskVV 
<VV 
IActionResultVV '
>VV' (
DeleteAddressVV) 6
(VV6 7
intVV7 :
idVV; =
)VV= >
{WW 	
varXX 
deletedXX 
=XX 
awaitXX 
_addressesManagerXX  1
.XX1 2
DeleteAddressAsyncXX2 D
(XXD E
idXXE G
)XXG H
;XXH I
ifZZ 
(ZZ 
!ZZ 
deletedZZ 
)ZZ 
{[[ 
return\\ 
NotFound\\ 
(\\  
$"\\  "
$str\\" 2
{\\2 3
id\\3 5
}\\5 6
$str\\6 A
"\\A B
)\\B C
;\\C D
}]] 
return__ 
	NoContent__ 
(__ 
)__ 
;__ 
}`` 	
}aa 
}bb ±
h/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Controllers/AdminController.cs
	namespace 	
WebShopRestService
 
. 
Controllers (
{ 
[ 
	Authorize 
( 
Roles 
= 
$str &
)& '
]' (
[ 
Route 

(
 
$str 
) 
] 
[		 
ApiController		 
]		 
public

 

class

 
AdminController

  
:

! "
ControllerBase

# 1
{ 
[ 	
HttpGet	 
( 
$str !
)! "
]" #
public 
IActionResult 
GetDashboardData -
(- .
). /
{ 	
return 
Ok 
( 
new 
{ 
message #
=$ %
$str& I
}J K
)K L
;L M
} 	
},, 
}-- ˛+
m/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Controllers/CategoriesController.cs
	namespace 	
WebShopRestService
 
. 
Controllers (
{		 
[

 
Route

 

(


 
$str

 
)

 
]

 
[ 
ApiController 
] 
[ 
Produces 
( 
$str  
)  !
]! "
public 

class  
CategoriesController %
:& '
ControllerBase( 6
{ 
private 
readonly 
CategoriesManager *
_categoriesManager+ =
;= >
public  
CategoriesController #
(# $
CategoriesManager$ 5
categoriesManager6 G
)G H
{ 	
_categoriesManager 
=  
categoriesManager! 2
;2 3
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
ActionResult &
<& '
IEnumerable' 2
<2 3
Category3 ;
>; <
>< =
>= >
GetCategories? L
(L M
)M N
{ 	
var 

categories 
= 
await "
_categoriesManager# 5
.5 6!
GetAllCategoriesAsync6 K
(K L
)L M
;M N
if 
( 

categories 
== 
null "
)" #
{ 
return 
NotFound 
(  
)  !
;! "
} 
return!! 
Ok!! 
(!! 

categories!!  
)!!  !
;!!! "
}"" 	
[%% 	
HttpGet%%	 
(%% 
$str%% 
)%% 
]%% 
public&& 
async&& 
Task&& 
<&& 
ActionResult&& &
<&&& '
Category&&' /
>&&/ 0
>&&0 1
GetCategory&&2 =
(&&= >
int&&> A
id&&B D
)&&D E
{'' 	
var(( 
category(( 
=(( 
await((  
_categoriesManager((! 3
.((3 4 
GetCategoryByIdAsync((4 H
(((H I
id((I K
)((K L
;((L M
if** 
(** 
category** 
==** 
null**  
)**  !
{++ 
return,, 
NotFound,, 
(,,  
),,  !
;,,! "
}-- 
return// 
category// 
;// 
}00 	
[33 	
HttpPut33	 
(33 
$str33 
)33 
]33 
public44 
async44 
Task44 
<44 
IActionResult44 '
>44' (
PutCategory44) 4
(444 5
int445 8
id449 ;
,44; <
Category44= E
category44F N
)44N O
{55 	
if66 
(66 
id66 
!=66 
category66 
.66 

CategoryId66 )
)66) *
{77 
return88 

BadRequest88 !
(88! "
)88" #
;88# $
}99 
try;; 
{<< 
await== 
_categoriesManager== (
.==( )
UpdateCategoryAsync==) <
(==< =
category=== E
)==E F
;==F G
}>> 
catch?? 
(??  
KeyNotFoundException?? '
)??' (
{@@ 
returnAA 
NotFoundAA 
(AA  
)AA  !
;AA! "
}BB 
returnDD 
	NoContentDD 
(DD 
)DD 
;DD 
}EE 	
[HH 	
HttpPostHH	 
]HH 
publicII 
asyncII 
TaskII 
<II 
ActionResultII &
<II& '
CategoryII' /
>II/ 0
>II0 1
PostCategoryII2 >
(II> ?
CategoryII? G
categoryIIH P
)IIP Q
{JJ 	
awaitKK 
_categoriesManagerKK $
.KK$ %
AddCategoryAsyncKK% 5
(KK5 6
categoryKK6 >
)KK> ?
;KK? @
returnMM 
CreatedAtActionMM "
(MM" #
nameofMM# )
(MM) *
GetCategoryMM* 5
)MM5 6
,MM6 7
newMM8 ;
{MM< =
idMM> @
=MMA B
categoryMMC K
.MMK L

CategoryIdMML V
}MMW X
,MMX Y
categoryMMZ b
)MMb c
;MMc d
}NN 	
[QQ 	

HttpDeleteQQ	 
(QQ 
$strQQ 
)QQ 
]QQ 
publicRR 
asyncRR 
TaskRR 
<RR 
IActionResultRR '
>RR' (
DeleteCategoryRR) 7
(RR7 8
intRR8 ;
idRR< >
)RR> ?
{SS 	
tryTT 
{UU 
awaitVV 
_categoriesManagerVV (
.VV( )
DeleteCategoryAsyncVV) <
(VV< =
idVV= ?
)VV? @
;VV@ A
}WW 
catchXX 
(XX  
KeyNotFoundExceptionXX '
)XX' (
{YY 
returnZZ 
NotFoundZZ 
(ZZ  
)ZZ  !
;ZZ! "
}[[ 
return]] 
	NoContent]] 
(]] 
)]] 
;]] 
}^^ 	
}__ 
}`` Æ+
l/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Controllers/CustomersController.cs
	namespace 	
WebShopRestService
 
. 
Controllers (
{		 
[

 
Route

 

(


 
$str

 
)

 
]

 
[ 
ApiController 
] 
public 

class 
CustomersController $
:% &
ControllerBase' 5
{ 
private 
readonly 
CustomersManager )
_customersManager* ;
;; <
public 
CustomersController "
(" #
CustomersManager# 3
customersManager4 D
)D E
{ 	
_customersManager 
= 
customersManager  0
;0 1
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
ActionResult &
<& '
IEnumerable' 2
<2 3
Customer3 ;
>; <
>< =
>= >
GetCustomers? K
(K L
)L M
{ 	
var 
	customers 
= 
await !
_customersManager" 3
.3 4
GetAll4 :
(: ;
); <
;< =
if 
( 
	customers 
== 
null !
)! "
{ 
return 
NotFound 
(  
)  !
;! "
} 
return   
Ok   
(   
	customers   
)    
;    !
}!! 	
[$$ 	
HttpGet$$	 
($$ 
$str$$ 
)$$ 
]$$ 
public%% 
async%% 
Task%% 
<%% 
ActionResult%% &
<%%& '
Customer%%' /
>%%/ 0
>%%0 1
GetCustomer%%2 =
(%%= >
int%%> A
id%%B D
)%%D E
{&& 	
var'' 
customer'' 
='' 
await''  
_customersManager''! 2
.''2 3
Get''3 6
(''6 7
id''7 9
)''9 :
;'': ;
if)) 
()) 
customer)) 
==)) 
null))  
)))  !
{** 
return++ 
NotFound++ 
(++  
)++  !
;++! "
},, 
return.. 
customer.. 
;.. 
}// 	
[22 	
HttpPut22	 
(22 
$str22 
)22 
]22 
public33 
async33 
Task33 
<33 
IActionResult33 '
>33' (
PutCustomer33) 4
(334 5
int335 8
id339 ;
,33; <
Customer33= E
customer33F N
)33N O
{44 	
if55 
(55 
id55 
!=55 
customer55 
.55 

CustomerId55 )
)55) *
{66 
return77 

BadRequest77 !
(77! "
)77" #
;77# $
}88 
try:: 
{;; 
await<< 
_customersManager<< '
.<<' (
Update<<( .
(<<. /
id<</ 1
,<<1 2
customer<<3 ;
)<<; <
;<<< =
}== 
catch>> 
(>>  
KeyNotFoundException>> '
)>>' (
{?? 
return@@ 
NotFound@@ 
(@@  
)@@  !
;@@! "
}AA 
returnCC 
	NoContentCC 
(CC 
)CC 
;CC 
}DD 	
[GG 	
HttpPostGG	 
]GG 
publicHH 
asyncHH 
TaskHH 
<HH 
ActionResultHH &
<HH& '
CustomerHH' /
>HH/ 0
>HH0 1
PostCustomerHH2 >
(HH> ?
CustomerHH? G
customerHHH P
)HHP Q
{II 	
varJJ 
createdCustomerJJ 
=JJ  !
awaitJJ" '
_customersManagerJJ( 9
.JJ9 :
CreateJJ: @
(JJ@ A
customerJJA I
)JJI J
;JJJ K
returnLL 
CreatedAtActionLL "
(LL" #
nameofLL# )
(LL) *
GetCustomerLL* 5
)LL5 6
,LL6 7
newLL8 ;
{LL< =
idLL> @
=LLA B
createdCustomerLLC R
.LLR S

CustomerIdLLS ]
}LL^ _
,LL_ `
createdCustomerLLa p
)LLp q
;LLq r
}MM 	
[PP 	

HttpDeletePP	 
(PP 
$strPP 
)PP 
]PP 
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
IActionResultQQ '
>QQ' (
DeleteCustomerQQ) 7
(QQ7 8
intQQ8 ;
idQQ< >
)QQ> ?
{RR 	
trySS 
{TT 
awaitUU 
_customersManagerUU '
.UU' (
DeleteUU( .
(UU. /
idUU/ 1
)UU1 2
;UU2 3
}VV 
catchWW 
(WW  
KeyNotFoundExceptionWW '
)WW' (
{XX 
returnYY 
NotFoundYY 
(YY  
)YY  !
;YY! "
}ZZ 
return\\ 
	NoContent\\ 
(\\ 
)\\ 
;\\ 
}]] 	
}^^ 
}__ Ω1
m/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Controllers/OrderItemsController.cs
	namespace		 	
WebShopRestService		
 
.		 
Controllers		 (
{

 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
public 

class  
OrderItemsController %
:& '
ControllerBase( 6
{ 
private 
readonly 
OrderItemsManager *
_orderItemsManager+ =
;= >
public  
OrderItemsController #
(# $
OrderItemsManager$ 5
orderItemsManager6 G
)G H
{ 	
_orderItemsManager 
=  
orderItemsManager! 2
;2 3
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
ActionResult &
<& '
IEnumerable' 2
<2 3
	OrderItem3 <
>< =
>= >
>> ?
GetOrderItems@ M
(M N
)N O
{ 	
var 

orderItems 
= 
await "
_orderItemsManager# 5
.5 6!
GetAllOrderItemsAsync6 K
(K L
)L M
;M N
if 
( 

orderItems 
== 
null "
)" #
{ 
return 
NotFound 
(  
)  !
;! "
} 
return!! 
Ok!! 
(!! 

orderItems!!  
)!!  !
;!!! "
}"" 	
[%% 	
HttpGet%%	 
(%% 
$str%% 
)%% 
]%% 
public&& 
async&& 
Task&& 
<&& 
ActionResult&& &
<&&& '
	OrderItem&&' 0
>&&0 1
>&&1 2
GetOrderItem&&3 ?
(&&? @
int&&@ C
id&&D F
)&&F G
{'' 	
var(( 
	orderItem(( 
=(( 
await(( !
_orderItemsManager((" 4
.((4 5!
GetOrderItemByIdAsync((5 J
(((J K
id((K M
)((M N
;((N O
if** 
(** 
	orderItem** 
==** 
null** !
)**! "
{++ 
return,, 
NotFound,, 
(,,  
),,  !
;,,! "
}-- 
return// 
	orderItem// 
;// 
}00 	
[33 	
HttpPut33	 
(33 
$str33 
)33 
]33 
public44 
async44 
Task44 
<44 
IActionResult44 '
>44' (
PutOrderItem44) 5
(445 6
int446 9
id44: <
,44< =
	OrderItem44> G
	orderItem44H Q
)44Q R
{55 	
if66 
(66 
id66 
!=66 
	orderItem66 
.66  
OrderItemId66  +
)66+ ,
{77 
return88 

BadRequest88 !
(88! "
)88" #
;88# $
}99 
try;; 
{<< 
await== 
_orderItemsManager== (
.==( ) 
UpdateOrderItemAsync==) =
(=== >
	orderItem==> G
)==G H
;==H I
}>> 
catch?? 
(?? (
DbUpdateConcurrencyException?? /
)??/ 0
{@@ 
varAA 
existsAA 
=AA 
awaitAA "
_orderItemsManagerAA# 5
.AA5 6!
GetOrderItemByIdAsyncAA6 K
(AAK L
idAAL N
)AAN O
!=AAP R
nullAAS W
;AAW X
ifBB 
(BB 
!BB 
existsBB 
)BB 
{CC 
returnDD 
NotFoundDD #
(DD# $
)DD$ %
;DD% &
}EE 
elseFF 
{GG 
throwHH 
;HH 
}II 
}JJ 
returnLL 
	NoContentLL 
(LL 
)LL 
;LL 
}MM 	
[PP 	
HttpPostPP	 
]PP 
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
ActionResultQQ &
<QQ& '
	OrderItemQQ' 0
>QQ0 1
>QQ1 2
PostOrderItemQQ3 @
(QQ@ A
	OrderItemQQA J
	orderItemQQK T
)QQT U
{RR 	
trySS 
{TT 
awaitUU 
_orderItemsManagerUU (
.UU( )#
ValidateAndAddOrderItemUU) @
(UU@ A
	orderItemUUA J
)UUJ K
;UUK L
}VV 
catchWW 
(WW %
InvalidOperationExceptionWW ,
exWW- /
)WW/ 0
{XX 
returnYY 

BadRequestYY !
(YY! "
exYY" $
.YY$ %
MessageYY% ,
)YY, -
;YY- .
}ZZ 
return\\ 
CreatedAtAction\\ "
(\\" #
nameof\\# )
(\\) *
GetOrderItem\\* 6
)\\6 7
,\\7 8
new\\9 <
{\\= >
id\\? A
=\\B C
	orderItem\\D M
.\\M N
OrderItemId\\N Y
}\\Z [
,\\[ \
	orderItem\\] f
)\\f g
;\\g h
}]] 	
[`` 	

HttpDelete``	 
(`` 
$str`` 
)`` 
]`` 
publicaa 
asyncaa 
Taskaa 
<aa 
IActionResultaa '
>aa' (
DeleteOrderItemaa) 8
(aa8 9
intaa9 <
idaa= ?
)aa? @
{bb 	
trycc 
{dd 
awaitee 
_orderItemsManageree (
.ee( ) 
DeleteOrderItemAsyncee) =
(ee= >
idee> @
)ee@ A
;eeA B
}ff 
catchgg 
(gg  
KeyNotFoundExceptiongg '
)gg' (
{hh 
returnii 
NotFoundii 
(ii  
)ii  !
;ii! "
}jj 
returnll 
	NoContentll 
(ll 
)ll 
;ll 
}mm 	
}nn 
}oo ˛1
n/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Controllers/OrderTablesController.cs
	namespace		 	
WebShopRestService		
 
.		 
Controllers		 (
{

 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
public 

class !
OrderTablesController &
:' (
ControllerBase) 7
{ 
private 
readonly 
OrderTablesManager +
_orderTablesManager, ?
;? @
public !
OrderTablesController $
($ %
OrderTablesManager% 7
orderTablesManager8 J
)J K
{ 	
_orderTablesManager 
=  !
orderTablesManager" 4
;4 5
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
ActionResult &
<& '
IEnumerable' 2
<2 3

OrderTable3 =
>= >
>> ?
>? @
	GetOrdersA J
(J K
)K L
{ 	
var 
orders 
= 
await 
_orderTablesManager 2
.2 3
GetAllOrdersAsync3 D
(D E
)E F
;F G
if 
( 
orders 
== 
null 
) 
{ 
return 
NotFound 
(  
)  !
;! "
} 
return!! 
Ok!! 
(!! 
orders!! 
)!! 
;!! 
}"" 	
[%% 	
HttpGet%%	 
(%% 
$str%% 
)%% 
]%% 
public&& 
async&& 
Task&& 
<&& 
ActionResult&& &
<&&& '

OrderTable&&' 1
>&&1 2
>&&2 3
GetOrderTable&&4 A
(&&A B
int&&B E
id&&F H
)&&H I
{'' 	
var(( 

orderTable(( 
=(( 
await(( "
_orderTablesManager((# 6
.((6 7
GetOrderByIdAsync((7 H
(((H I
id((I K
)((K L
;((L M
if** 
(** 

orderTable** 
==** 
null** "
)**" #
{++ 
return,, 
NotFound,, 
(,,  
),,  !
;,,! "
}-- 
return// 

orderTable// 
;// 
}00 	
[33 	
HttpPut33	 
(33 
$str33 
)33 
]33 
public44 
async44 
Task44 
<44 
IActionResult44 '
>44' (
PutOrderTable44) 6
(446 7
int447 :
id44; =
,44= >

OrderTable44? I

orderTable44J T
)44T U
{55 	
if66 
(66 
id66 
!=66 

orderTable66  
.66  !
OrderId66! (
)66( )
{77 
return88 

BadRequest88 !
(88! "
)88" #
;88# $
}99 
try;; 
{<< 
await== 
_orderTablesManager== )
.==) *
UpdateOrderAsync==* :
(==: ;

orderTable==; E
)==E F
;==F G
}>> 
catch?? 
(?? (
DbUpdateConcurrencyException?? /
)??/ 0
{@@ 
varAA 
existsAA 
=AA 
awaitAA "
_orderTablesManagerAA# 6
.AA6 7
GetOrderByIdAsyncAA7 H
(AAH I
idAAI K
)AAK L
!=AAM O
nullAAP T
;AAT U
ifBB 
(BB 
!BB 
existsBB 
)BB 
{CC 
returnDD 
NotFoundDD #
(DD# $
)DD$ %
;DD% &
}EE 
elseFF 
{GG 
throwHH 
;HH 
}II 
}JJ 
returnLL 
	NoContentLL 
(LL 
)LL 
;LL 
}MM 	
[PP 	
HttpPostPP	 
]PP 
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
ActionResultQQ &
<QQ& '

OrderTableQQ' 1
>QQ1 2
>QQ2 3
PostOrderTableQQ4 B
(QQB C

OrderTableQQC M

orderTableQQN X
)QQX Y
{RR 	
trySS 
{TT 
awaitUU 
_orderTablesManagerUU )
.UU) *$
ValidateAndAddOrderAsyncUU* B
(UUB C

orderTableUUC M
)UUM N
;UUN O
}VV 
catchWW 
(WW %
InvalidOperationExceptionWW ,
exWW- /
)WW/ 0
{XX 
returnYY 

BadRequestYY !
(YY! "
exYY" $
.YY$ %
MessageYY% ,
)YY, -
;YY- .
}ZZ 
return\\ 
CreatedAtAction\\ "
(\\" #
nameof\\# )
(\\) *
GetOrderTable\\* 7
)\\7 8
,\\8 9
new\\: =
{\\> ?
id\\@ B
=\\C D

orderTable\\E O
.\\O P
OrderId\\P W
}\\X Y
,\\Y Z

orderTable\\[ e
)\\e f
;\\f g
}]] 	
[`` 	

HttpDelete``	 
(`` 
$str`` 
)`` 
]`` 
publicaa 
asyncaa 
Taskaa 
<aa 
IActionResultaa '
>aa' (
DeleteOrderTableaa) 9
(aa9 :
intaa: =
idaa> @
)aa@ A
{bb 	
trycc 
{dd 
awaitee 
_orderTablesManageree )
.ee) *
DeleteOrderAsyncee* :
(ee: ;
idee; =
)ee= >
;ee> ?
}ff 
catchgg 
(gg %
InvalidOperationExceptiongg ,
exgg- /
)gg/ 0
{hh 
returnii 
NotFoundii 
(ii  
exii  "
.ii" #
Messageii# *
)ii* +
;ii+ ,
}jj 
returnll 
	NoContentll 
(ll 
)ll 
;ll 
}mm 	
}nn 
}oo ƒ9
k/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Controllers/ProductsController.cs
	namespace		 	
WebShopRestService		
 
.		 
Controllers		 (
{

 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
public 

class 
ProductsController #
:$ %
ControllerBase& 4
{ 
private 
readonly 
ProductsManager (
_productsManager) 9
;9 :
private 
readonly 
IMapper  
_mapper! (
;( )
public 
ProductsController !
(! "
ProductsManager" 1
productsManager2 A
,A B
IMapperC J
mapperK Q
)Q R
{ 	
_productsManager 
= 
productsManager .
;. /
_mapper 
= 
mapper 
; 
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
ActionResult &
<& '
IEnumerable' 2
<2 3

ProductDTO3 =
>= >
>> ?
>? @
GetProductsA L
(L M
)M N
{ 	
var 
products 
= 
await  
_productsManager! 1
.1 2
GetAll2 8
(8 9
)9 :
;: ;
if 
( 
products 
== 
null  
)  !
{ 
return 
NotFound 
(  
)  !
;! "
}   
var!! 
results!! 
=!! 
_mapper!! !
.!!! "
Map!!" %
<!!% &
IList!!& +
<!!+ ,

ProductDTO!!, 6
>!!6 7
>!!7 8
(!!8 9
products!!9 A
)!!A B
;!!B C
return"" 
Ok"" 
("" 
results"" 
)"" 
;"" 
}## 	
[&& 	
HttpGet&&	 
(&& 
$str&& 
)&& 
]&& 
public'' 
async'' 
Task'' 
<'' 
ActionResult'' &
<''& '

ProductDTO''' 1
>''1 2
>''2 3

GetProduct''4 >
(''> ?
int''? B
id''C E
)''E F
{(( 	
var)) 
product)) 
=)) 
await)) 
_productsManager))  0
.))0 1
Get))1 4
())4 5
id))5 7
)))7 8
;))8 9
if** 
(** 
product** 
==** 
null** 
)**  
{++ 
return,, 
NotFound,, 
(,,  
),,  !
;,,! "
}-- 
var.. 
result.. 
=.. 
_mapper..  
...  !
Map..! $
<..$ %

ProductDTO..% /
>../ 0
(..0 1
product..1 8
)..8 9
;..9 :
return// 
Ok// 
(// 
result// 
)// 
;// 
}00 	
[33 	
HttpPut33	 
(33 
$str33 
)33 
]33 
public44 
async44 
Task44 
<44 
IActionResult44 '
>44' (

PutProduct44) 3
(443 4
int444 7
id448 :
,44: ;
[44< =
FromBody44= E
]44E F

ProductDTO44G Q
updates44R Y
)44Y Z
{55 	
if66 
(66 
id66 
!=66 
updates66 
.66 
	ProductId66 '
)66' (
{77 
return88 

BadRequest88 !
(88! "
)88" #
;88# $
}99 
var;; 
productToUpdate;; 
=;;  !
await;;" '
_productsManager;;( 8
.;;8 9
Get;;9 <
(;;< =
id;;= ?
);;? @
;;;@ A
if<< 
(<< 
productToUpdate<< 
==<<  "
null<<# '
)<<' (
{== 
return>> 
NotFound>> 
(>>  
)>>  !
;>>! "
}?? 
_mapperAA 
.AA 
MapAA 
(AA 
updatesAA 
,AA  
productToUpdateAA! 0
)AA0 1
;AA1 2
awaitBB 
_productsManagerBB "
.BB" #
UpdateBB# )
(BB) *
idBB* ,
,BB, -
productToUpdateBB. =
)BB= >
;BB> ?
returnDD 
	NoContentDD 
(DD 
)DD 
;DD 
}EE 	
[HH 	
HttpPostHH	 
]HH 
publicII 
asyncII 
TaskII 
<II 
ActionResultII &
<II& '

ProductDTOII' 1
>II1 2
>II2 3
PostProductII4 ?
(II? @
[II@ A
FromBodyIIA I
]III J

ProductDTOIIK U

productDtoIIV `
)II` a
{JJ 	
varKK 
productKK 
=KK 
_mapperKK !
.KK! "
MapKK" %
<KK% &
ProductKK& -
>KK- .
(KK. /

productDtoKK/ 9
)KK9 :
;KK: ;
varLL 
createdProductLL 
=LL  
awaitLL! &
_productsManagerLL' 7
.LL7 8
CreateLL8 >
(LL> ?
productLL? F
)LLF G
;LLG H
varMM 
resultMM 
=MM 
_mapperMM  
.MM  !
MapMM! $
<MM$ %

ProductDTOMM% /
>MM/ 0
(MM0 1
createdProductMM1 ?
)MM? @
;MM@ A
returnOO 
CreatedAtActionOO "
(OO" #
nameofOO# )
(OO) *

GetProductOO* 4
)OO4 5
,OO5 6
newOO7 :
{OO; <
idOO= ?
=OO@ A
resultOOB H
.OOH I
	ProductIdOOI R
}OOS T
,OOT U
resultOOV \
)OO\ ]
;OO] ^
}PP 	
[SS 	

HttpDeleteSS	 
(SS 
$strSS 
)SS 
]SS 
publicTT 
asyncTT 
TaskTT 
<TT 
IActionResultTT '
>TT' (
DeleteProductTT) 6
(TT6 7
intTT7 :
idTT; =
)TT= >
{UU 	
varVV 
productVV 
=VV 
awaitVV 
_productsManagerVV  0
.VV0 1
GetVV1 4
(VV4 5
idVV5 7
)VV7 8
;VV8 9
ifWW 
(WW 
productWW 
==WW 
nullWW 
)WW  
{XX 
returnYY 
NotFoundYY 
(YY  
)YY  !
;YY! "
}ZZ 
await\\ 
_productsManager\\ "
.\\" #
Delete\\# )
(\\) *
id\\* ,
)\\, -
;\\- .
return^^ 
	NoContent^^ 
(^^ 
)^^ 
;^^ 
}__ 	
}`` 
}aa s
q/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Controllers/ProductsControllerBackUp.csÀ1
h/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Controllers/RolesController.cs
	namespace		 	
WebShopRestService		
 
.		 
Controllers		 (
{

 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str &
)& '
]' (
public 

class 
RolesController  
:! "
ControllerBase# 1
{ 
private 
readonly 
RolesManager %
_rolesManager& 3
;3 4
public 
RolesController 
( 
RolesManager +
rolesManager, 8
)8 9
{ 	
_rolesManager 
= 
rolesManager (
;( )
} 	
[ 	
HttpGet	 
] 
[ 	
AllowAnonymous	 
] 
public 
async 
Task 
< 
ActionResult &
<& '
IEnumerable' 2
<2 3
Role3 7
>7 8
>8 9
>9 :
GetRoles; C
(C D
)D E
{ 	
var 
roles 
= 
await 
_rolesManager +
.+ ,
GetAll, 2
(2 3
)3 4
;4 5
return 
Ok 
( 
roles 
) 
; 
} 	
[!! 	
HttpGet!!	 
(!! 
$str!! 
)!! 
]!! 
["" 	
AllowAnonymous""	 
]"" 
public## 
async## 
Task## 
<## 
ActionResult## &
<##& '
Role##' +
>##+ ,
>##, -
GetRole##. 5
(##5 6
int##6 9
id##: <
)##< =
{$$ 	
var%% 
role%% 
=%% 
await%% 
_rolesManager%% *
.%%* +
Get%%+ .
(%%. /
id%%/ 1
)%%1 2
;%%2 3
if'' 
('' 
role'' 
=='' 
null'' 
)'' 
{(( 
return)) 
NotFound)) 
())  
)))  !
;))! "
}** 
return,, 
role,, 
;,, 
}-- 	
[00 	
HttpPut00	 
(00 
$str00 
)00 
]00 
public11 
async11 
Task11 
<11 
IActionResult11 '
>11' (
PutRole11) 0
(110 1
int111 4
id115 7
,117 8
Role119 =
role11> B
)11B C
{22 	
if33 
(33 
id33 
!=33 
role33 
.33 
RoleId33 !
)33! "
{44 
return55 

BadRequest55 !
(55! "
)55" #
;55# $
}66 
try88 
{99 
await:: 
_rolesManager:: #
.::# $
Update::$ *
(::* +
id::+ -
,::- .
role::/ 3
)::3 4
;::4 5
};; 
catch<< 
(<< (
DbUpdateConcurrencyException<< /
)<</ 0
{== 
if>> 
(>> 
!>> 
await>> 

RoleExists>> %
(>>% &
id>>& (
)>>( )
)>>) *
{?? 
return@@ 
NotFound@@ #
(@@# $
)@@$ %
;@@% &
}AA 
elseBB 
{CC 
throwDD 
;DD 
}EE 
}FF 
returnHH 
	NoContentHH 
(HH 
)HH 
;HH 
}II 	
[LL 	
HttpPostLL	 
]LL 
publicMM 
asyncMM 
TaskMM 
<MM 
ActionResultMM &
<MM& '
RoleMM' +
>MM+ ,
>MM, -
PostRoleMM. 6
(MM6 7
RoleMM7 ;
roleMM< @
)MM@ A
{NN 	
varOO 
createdRoleOO 
=OO 
awaitOO #
_rolesManagerOO$ 1
.OO1 2
CreateOO2 8
(OO8 9
roleOO9 =
)OO= >
;OO> ?
returnPP 
CreatedAtActionPP "
(PP" #
nameofPP# )
(PP) *
GetRolePP* 1
)PP1 2
,PP2 3
newPP4 7
{PP8 9
idPP: <
=PP= >
createdRolePP? J
.PPJ K
RoleIdPPK Q
}PPR S
,PPS T
createdRolePPU `
)PP` a
;PPa b
}QQ 	
[TT 	

HttpDeleteTT	 
(TT 
$strTT 
)TT 
]TT 
publicUU 
asyncUU 
TaskUU 
<UU 
IActionResultUU '
>UU' (

DeleteRoleUU) 3
(UU3 4
intUU4 7
idUU8 :
)UU: ;
{VV 	
varWW 
roleWW 
=WW 
awaitWW 
_rolesManagerWW *
.WW* +
GetWW+ .
(WW. /
idWW/ 1
)WW1 2
;WW2 3
ifXX 
(XX 
roleXX 
==XX 
nullXX 
)XX 
{YY 
returnZZ 
NotFoundZZ 
(ZZ  
)ZZ  !
;ZZ! "
}[[ 
await]] 
_rolesManager]] 
.]]  
Delete]]  &
(]]& '
id]]' )
)]]) *
;]]* +
return^^ 
	NoContent^^ 
(^^ 
)^^ 
;^^ 
}__ 	
privateaa 
asyncaa 
Taskaa 
<aa 
boolaa 
>aa  

RoleExistsaa! +
(aa+ ,
intaa, /
idaa0 2
)aa2 3
{bb 	
varcc 
rolecc 
=cc 
awaitcc 
_rolesManagercc *
.cc* +
Getcc+ .
(cc. /
idcc/ 1
)cc1 2
;cc2 3
returndd 
roledd 
!=dd 
nulldd 
;dd  
}ee 	
}ff 
}gg ≈P
r/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Controllers/UserCredentialsController.cs
	namespace 	
WebShopRestService
 
. 
Controllers (
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
public 

class %
UserCredentialsController *
:+ ,
ControllerBase- ;
{ 
private 
readonly 
MyDbContext $
_context% -
;- .
private 
readonly "
UserCredentialsManager /#
_userCredentialsManager0 G
;G H
public %
UserCredentialsController (
(( )
MyDbContext) 4
context5 <
,< ="
UserCredentialsManager> T"
userCredentialsManagerU k
)k l
{ 	
_context 
= 
context 
; #
_userCredentialsManager #
=$ %"
userCredentialsManager& <
;< =
} 	
[ 	
HttpPost	 
( 
$str 
) 
] 
public 
async 
Task 
< 
ActionResult &
<& '
UserCredential' 5
>5 6
>6 7
Register8 @
(@ A
UserCredentialA O
userCredentialP ^
)^ _
{   	
userCredential"" 
."" 
HashedPassword"" )
=""* +#
_userCredentialsManager"", C
.""C D
HashPassword""D P
(""P Q
userCredential""Q _
.""_ `
HashedPassword""` n
)""n o
;""o p
_context$$ 
.$$ 
UserCredentials$$ $
.$$$ %
Add$$% (
($$( )
userCredential$$) 7
)$$7 8
;$$8 9
await%% 
_context%% 
.%% 
SaveChangesAsync%% +
(%%+ ,
)%%, -
;%%- .
return'' 
CreatedAtAction'' "
(''" #
$str''# 6
,''6 7
new''8 ;
{''< =
id''> @
=''A B
userCredential''C Q
.''Q R
UserId''R X
}''Y Z
,''Z [
userCredential''\ j
)''j k
;''k l
}(( 	
[++ 	
AllowAnonymous++	 
]++ 
[,, 	
HttpPost,,	 
(,, 
$str,, 
),, 
],, 
public-- 
async-- 
Task-- 
<-- 
ActionResult-- &
<--& '
string--' -
>--- .
>--. /
Login--0 5
(--5 6
[--6 7
FromBody--7 ?
]--? @
LoginDto--A I
loginDto--J R
)--R S
{.. 	
var00 
userCredential00 
=00  
await00! &
_context00' /
.00/ 0
UserCredentials000 ?
.11/ 0 
SingleOrDefaultAsync110 D
(11D E
u11E F
=>11G I
u11J K
.11K L
Username11L T
==11U W
loginDto11X `
.11` a
Username11a i
)11i j
;11j k
if44 
(44 
userCredential44 
==44 !
null44" &
||44' )
!44* +#
_userCredentialsManager44+ B
.44B C
VerifyPassword44C Q
(44Q R
loginDto44R Z
.44Z [
Password44[ c
,44c d
userCredential44e s
.44s t
HashedPassword	44t Ç
)
44Ç É
)
44É Ñ
{55 
return66 
Unauthorized66 #
(66# $
$str66$ C
)66C D
;66D E
}77 
var:: 
token:: 
=:: #
_userCredentialsManager:: /
.::/ 0
GenerateJwtToken::0 @
(::@ A
userCredential::A O
)::O P
;::P Q
return;; 
Ok;; 
(;; 
new;; 
{;; 
token;; !
=;;" #
token;;$ )
,;;) *
role;;+ /
=;;0 1
userCredential;;2 @
.;;@ A
Role;;A E
.;;E F
Name;;F J
};;K L
);;L M
;;;M N
}<< 	
[?? 	
HttpGet??	 
]?? 
public@@ 
async@@ 
Task@@ 
<@@ 
ActionResult@@ &
<@@& '
IEnumerable@@' 2
<@@2 3
UserCredential@@3 A
>@@A B
>@@B C
>@@C D
GetUserCredentials@@E W
(@@W X
)@@X Y
{AA 	
ifBB 
(BB 
_contextBB 
.BB 
UserCredentialsBB (
==BB) +
nullBB, 0
)BB0 1
{CC 
returnDD 
NotFoundDD 
(DD  
)DD  !
;DD! "
}EE 
returnFF 
awaitFF 
_contextFF !
.FF! "
UserCredentialsFF" 1
.FF1 2
ToListAsyncFF2 =
(FF= >
)FF> ?
;FF? @
}GG 	
[JJ 	
HttpGetJJ	 
(JJ 
$strJJ 
)JJ 
]JJ 
publicKK 
asyncKK 
TaskKK 
<KK 
ActionResultKK &
<KK& '
UserCredentialKK' 5
>KK5 6
>KK6 7
GetUserCredentialKK8 I
(KKI J
intKKJ M
idKKN P
)KKP Q
{LL 	
ifMM 
(MM 
_contextMM 
.MM 
UserCredentialsMM (
==MM) +
nullMM, 0
)MM0 1
{NN 
returnOO 
NotFoundOO 
(OO  
)OO  !
;OO! "
}PP 
varQQ 
userCredentialQQ 
=QQ  
awaitQQ! &
_contextQQ' /
.QQ/ 0
UserCredentialsQQ0 ?
.QQ? @
	FindAsyncQQ@ I
(QQI J
idQQJ L
)QQL M
;QQM N
ifSS 
(SS 
userCredentialSS 
==SS !
nullSS" &
)SS& '
{TT 
returnUU 
NotFoundUU 
(UU  
)UU  !
;UU! "
}VV 
returnXX 
userCredentialXX !
;XX! "
}YY 	
[\\ 	
HttpPut\\	 
(\\ 
$str\\ 
)\\ 
]\\ 
public]] 
async]] 
Task]] 
<]] 
IActionResult]] '
>]]' (
PutUserCredential]]) :
(]]: ;
int]]; >
id]]? A
,]]A B
UserCredential]]C Q
userCredential]]R `
)]]` a
{^^ 	
if__ 
(__ 
id__ 
!=__ 
userCredential__ $
.__$ %
UserId__% +
)__+ ,
{`` 
returnaa 

BadRequestaa !
(aa! "
)aa" #
;aa# $
}bb 
_contextdd 
.dd 
Entrydd 
(dd 
userCredentialdd )
)dd) *
.dd* +
Statedd+ 0
=dd1 2
EntityStatedd3 >
.dd> ?
Modifieddd? G
;ddG H
tryff 
{gg 
awaithh 
_contexthh 
.hh 
SaveChangesAsynchh /
(hh/ 0
)hh0 1
;hh1 2
}ii 
catchjj 
(jj (
DbUpdateConcurrencyExceptionjj /
)jj/ 0
{kk 
ifll 
(ll 
!ll  
UserCredentialExistsll )
(ll) *
idll* ,
)ll, -
)ll- .
{mm 
returnnn 
NotFoundnn #
(nn# $
)nn$ %
;nn% &
}oo 
elsepp 
{qq 
throwrr 
;rr 
}ss 
}tt 
returnvv 
	NoContentvv 
(vv 
)vv 
;vv 
}ww 	
[zz 	

HttpDeletezz	 
(zz 
$strzz 
)zz 
]zz 
public{{ 
async{{ 
Task{{ 
<{{ 
IActionResult{{ '
>{{' ( 
DeleteUserCredential{{) =
({{= >
int{{> A
id{{B D
){{D E
{|| 	
if}} 
(}} 
_context}} 
.}} 
UserCredentials}} (
==}}) +
null}}, 0
)}}0 1
{~~ 
return 
NotFound 
(  
)  !
;! "
}
ÄÄ 
var
ÅÅ 
userCredential
ÅÅ 
=
ÅÅ  
await
ÅÅ! &
_context
ÅÅ' /
.
ÅÅ/ 0
UserCredentials
ÅÅ0 ?
.
ÅÅ? @
	FindAsync
ÅÅ@ I
(
ÅÅI J
id
ÅÅJ L
)
ÅÅL M
;
ÅÅM N
if
ÇÇ 
(
ÇÇ 
userCredential
ÇÇ 
==
ÇÇ !
null
ÇÇ" &
)
ÇÇ& '
{
ÉÉ 
return
ÑÑ 
NotFound
ÑÑ 
(
ÑÑ  
)
ÑÑ  !
;
ÑÑ! "
}
ÖÖ 
_context
áá 
.
áá 
UserCredentials
áá $
.
áá$ %
Remove
áá% +
(
áá+ ,
userCredential
áá, :
)
áá: ;
;
áá; <
await
àà 
_context
àà 
.
àà 
SaveChangesAsync
àà +
(
àà+ ,
)
àà, -
;
àà- .
return
ää 
	NoContent
ää 
(
ää 
)
ää 
;
ää 
}
ãã 	
private
éé 
bool
éé "
UserCredentialExists
éé )
(
éé) *
int
éé* -
id
éé. 0
)
éé0 1
{
èè 	
return
êê 
(
êê 
_context
êê 
.
êê 
UserCredentials
êê ,
?
êê, -
.
êê- .
Any
êê. 1
(
êê1 2
e
êê2 3
=>
êê4 6
e
êê7 8
.
êê8 9
UserId
êê9 ?
==
êê@ B
id
êêC E
)
êêE F
)
êêF G
.
êêG H
GetValueOrDefault
êêH Y
(
êêY Z
)
êêZ [
;
êê[ \
}
ëë 	
}
íí 
}ìì Ã
]/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Data/MyDbContext.cs
	namespace 	
WebShopRestService
 
. 
Data !
{ 
public 

class 
MyDbContext 
: 
	DbContext (
{ 
public 
MyDbContext 
( 
DbContextOptions +
<+ ,
MyDbContext, 7
>7 8
options9 @
)@ A
:		 
base		 
(		 
options		 
)		 
{

 	
} 	
public 
DbSet 
< 
Category 
> 

Categories )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
DbSet 
< 
Address 
> 
	Addresses '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
DbSet 
< 
UserCredential #
># $
UserCredentials% 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
DbSet 
< 
Customer 
> 
	Customers (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
DbSet 
< 
Product 
> 
Products &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
DbSet 
< 

OrderTable 
>  
OrderTables! ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
DbSet 
< 
	OrderItem 
> 

OrderItems  *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
DbSet 
< 
Payment 
> 
Payments &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
DbSet 
< 
ProductAudit !
>! "
ProductAudits# 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
DbSet 
< 
Role 
> 
Roles  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
DbSet 
< 
PaymentAudit !
>! "
PaymentAudits# 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
	protected 
override 
void 
OnModelCreating  /
(/ 0
ModelBuilder0 <
modelBuilder= I
)I J
{ 	
} 	
} 
} ﬁ
Y/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/DTO/LoginDTO.cs
	namespace 	
WebShopRestService
 
. 
DTOs !
{ 
public 

class 
LoginDto 
{ 
public 
string 
Username 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} Œ
[/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/DTO/ProductDTO.cs
	namespace 	
WebShopRestService
 
. 
DTO  
{ 
public 

class 

ProductDTO 
{ 
[		 	
Key			 
]		 
public

 
int

 
	ProductId

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* ^
)^ _
]_ `
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* U
)U V
]V W
[ 	
DataType	 
( 
DataType 
. 
MultilineText (
)( )
]) *
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
StringLength	 
( 
$num 
) 
] 
public 
string 
Img 
{ 
get 
;  
set! $
;$ %
}& '
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 5
)5 6
]6 7
[ 	
Column	 
( 
TypeName 
= 
$str +
)+ ,
], -
[ 	
Range	 
( 
$num 
, 
$num 
,  
ErrorMessage! -
=. /
$str0 [
)[ \
]\ ]
public 
decimal 
Price 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Required	 
( 
ErrorMessage 
=  
$str! >
)> ?
]? @
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ S
)S T
]T U
public 
int 
StockQuantity  
{! "
get# &
;& '
set( +
;+ ,
}- .
[   	
Required  	 
(   
ErrorMessage   
=    
$str  ! ;
)  ; <
]  < =
[!! 	

ForeignKey!!	 
(!! 
$str!! 
)!! 
]!!  
public"" 
int"" 

CategoryId"" 
{"" 
get""  #
;""# $
set""% (
;""( )
}""* +
}## 
public%% 

class%% 
CreateProductDTO%% !
:%%" #

ProductDTO%%$ .
{&& 
public'' 
Category'' 
Category''  
{''! "
get''# &
;''& '
set''( +
;''+ ,
}''- .
public** 
ICollection** 
<** 
	OrderItem** $
>**$ %

OrderItems**& 0
{**1 2
get**3 6
;**6 7
set**8 ;
;**; <
}**= >
}++ 
},, §

l/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Interfaces/IAddressesRepository.cs
	namespace 	
WebShopRestService
 
. 

Interfaces '
{ 
public 

	interface  
IAddressesRepository )
{ 
Task		 
<		 
IEnumerable		 
<		 
Address		  
>		  !
>		! " 
GetAllAddressesAsync		# 7
(		7 8
)		8 9
;		9 :
Task

 
<

 
Address

 
>

 
GetAddressByIdAsync

 )
(

) *
int

* -
	addressId

. 7
)

7 8
;

8 9
Task 
AddAddressAsync 
( 
Address $
address% ,
), -
;- .
Task 
UpdateAddressAsync 
(  
Address  '
address( /
)/ 0
;0 1
Task 
DeleteAddressAsync 
(  
Address  '
address( /
)/ 0
;0 1
Task 
< 
bool 
> 
AddressExistsAsync %
(% &
int& )
	addressId* 3
)3 4
;4 5
} 
} ˙
m/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Interfaces/ICategoriesRepository.cs
	namespace 	
WebShopRestService
 
. 

Interfaces '
{ 
public 

	interface !
ICategoriesRepository *
{ 
Task		 
<		 
IEnumerable		 
<		 
Category		 !
>		! "
>		" #!
GetAllCategoriesAsync		$ 9
(		9 :
)		: ;
;		; <
Task

 
<

 
Category

 
>

  
GetCategoryByIdAsync

 +
(

+ ,
int

, /

categoryId

0 :
)

: ;
;

; <
Task 
AddCategoryAsync 
( 
Category &
category' /
)/ 0
;0 1
Task 
UpdateCategoryAsync  
(  !
Category! )
category* 2
)2 3
;3 4
Task 
DeleteCategoryAsync  
(  !
int! $

categoryId% /
)/ 0
;0 1
} 
} £

l/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Interfaces/ICustomersRepository.cs
	namespace 	
WebShopRestService
 
. 

Interfaces '
{ 
public 

	interface  
ICustomersRepository )
{		 
Task

 
<

 
IEnumerable

 
<

 
Customer

 !
>

! "
>

" # 
GetAllCustomersAsync

$ 8
(

8 9
)

9 :
;

: ;
Task 
< 
Customer 
>  
GetCustomerByIdAsync +
(+ ,
int, /

customerId0 :
): ;
;; <
Task 
AddCustomerAsync 
( 
Customer &
customer' /
)/ 0
;0 1
Task 
UpdateCustomerAsync  
(  !
Customer! )
customer* 2
)2 3
;3 4
Task 
DeleteCustomerAsync  
(  !
int! $

customerId% /
)/ 0
;0 1
Task 
< 
bool 
> 
Exists 
( 
int 

customerId (
)( )
;) *
} 
} Ü	
m/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Interfaces/IOrderItemsRepository.cs
	namespace 	
WebShopRestService
 
. 

Interfaces '
{ 
public 

	interface !
IOrderItemsRepository *
{ 
Task		 
<		 
IEnumerable		 
<		 
	OrderItem		 "
>		" #
>		# $!
GetAllOrderItemsAsync		% :
(		: ;
)		; <
;		< =
Task

 
<

 
	OrderItem

 
>

 !
GetOrderItemByIdAsync

 -
(

- .
int

. 1
orderItemId

2 =
)

= >
;

> ?
Task 
AddOrderItemAsync 
( 
	OrderItem (
	orderItem) 2
)2 3
;3 4
Task  
UpdateOrderItemAsync !
(! "
	OrderItem" +
	orderItem, 5
)5 6
;6 7
Task  
DeleteOrderItemAsync !
(! "
int" %
orderItemId& 1
)1 2
;2 3
} 
} ‡
n/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Interfaces/IOrderTablesRepository.cs
	namespace 	
WebShopRestService
 
. 

Interfaces '
{ 
public 

	interface "
IOrderTablesRepository +
{ 
Task		 
<		 
IEnumerable		 
<		 

OrderTable		 #
>		# $
>		$ %
GetAllOrdersAsync		& 7
(		7 8
)		8 9
;		9 :
Task

 
<

 

OrderTable

 
>

 
GetOrderByIdAsync

 *
(

* +
int

+ .
orderId

/ 6
)

6 7
;

7 8
Task 
AddOrderAsync 
( 

OrderTable %
order& +
)+ ,
;, -
Task 
UpdateOrderAsync 
( 

OrderTable (
order) .
). /
;/ 0
Task 
DeleteOrderAsync 
( 
int !
orderId" )
)) *
;* +
Task 
< 
IEnumerable 
< 

OrderTable #
># $
>$ %+
GetOrdersByCustomerAndDateAsync& E
(E F
intF I

customerIdJ T
,T U
DateTimeV ^
start_ d
,d e
DateTimef n
endo r
)r s
;s t
Task 
DeleteOrderAsync 
( 

OrderTable (
order) .
). /
;/ 0
} 
} ü

k/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Interfaces/IProductsRepository.cs
	namespace 	
WebShopRestService
 
. 

Interfaces '
{ 
public 

	interface 
IProductsRepository (
{		 
Task

 
<

 
IEnumerable

 
<

 
Product

  
>

  !
>

! "
GetAllProductsAsync

# 6
(

6 7
)

7 8
;

8 9
Task 
< 
Product 
> 
GetProductByIdAsync )
() *
int* -
	productId. 7
)7 8
;8 9
Task 
AddProductAsync 
( 
Product $
product% ,
), -
;- .
Task 
UpdateProductAsync 
(  
Product  '
product( /
)/ 0
;0 1
Task 
DeleteProductAsync 
(  
int  #
	productId$ -
)- .
;. /
Task 
< 
bool 
> 
ProductExistsAsync %
(% &
int& )
	productId* 3
)3 4
;4 5
} 
} ª
h/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Interfaces/IRolesRepository.cs
	namespace 	
WebShopRestService
 
. 

Interfaces '
{ 
public 

	interface 
IRolesRepository %
{		 
Task

 
<

 
IEnumerable

 
<

 
Role

 
>

 
>

 
GetAllRolesAsync

  0
(

0 1
)

1 2
;

2 3
Task 
< 
Role 
> 
GetRoleByIdAsync #
(# $
int$ '
roleId( .
). /
;/ 0
Task 
AddRoleAsync 
( 
Role 
role #
)# $
;$ %
Task 
UpdateRoleAsync 
( 
Role !
role" &
)& '
;' (
Task 
DeleteRoleAsync 
( 
int  
roleId! '
)' (
;( )
} 
} «
f/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Managers/AddressesManager.cs
	namespace 	
WebShopRestService
 
. 
Managers %
{ 
public 

class 
AddressesManager !
{		 
private

 
readonly

  
IAddressesRepository

 - 
_addressesRepository

. B
;

B C
public 
AddressesManager 
(   
IAddressesRepository  4
addressesRepository5 H
)H I
{ 	 
_addressesRepository  
=! "
addressesRepository# 6
;6 7
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Address& -
>- .
>. /
GetAddressesAsync0 A
(A B
)B C
{ 	
return 
await  
_addressesRepository -
.- . 
GetAllAddressesAsync. B
(B C
)C D
;D E
} 	
public 
async 
Task 
< 
Address !
>! "
GetAddressByIdAsync# 6
(6 7
int7 :
id; =
)= >
{ 	
return 
await  
_addressesRepository -
.- .
GetAddressByIdAsync. A
(A B
idB D
)D E
;E F
} 	
public 
async 
Task 
< 
bool 
> 
UpdateAddressAsync  2
(2 3
Address3 :
address; B
)B C
{ 	
var 
existingAddress 
=  !
await" ' 
_addressesRepository( <
.< =
GetAddressByIdAsync= P
(P Q
addressQ X
.X Y
	AddressIdY b
)b c
;c d
if 
( 
existingAddress 
==  "
null# '
)' (
{ 
return   
false   
;   
}!! 
await##  
_addressesRepository## &
.##& '
UpdateAddressAsync##' 9
(##9 :
address##: A
)##A B
;##B C
return$$ 
true$$ 
;$$ 
}%% 	
public'' 
async'' 
Task'' 
<'' 
Address'' !
>''! "
CreateAddressAsync''# 5
(''5 6
Address''6 =
address''> E
)''E F
{(( 	
await))  
_addressesRepository)) &
.))& '
AddAddressAsync))' 6
())6 7
address))7 >
)))> ?
;))? @
return++ 
address++ 
;++ 
},, 	
public.. 
async.. 
Task.. 
<.. 
bool.. 
>.. 
DeleteAddressAsync..  2
(..2 3
int..3 6
id..7 9
)..9 :
{// 	
var00 
address00 
=00 
await00  
_addressesRepository00  4
.004 5
GetAddressByIdAsync005 H
(00H I
id00I K
)00K L
;00L M
if11 
(11 
address11 
==11 
null11 
)11  
{22 
return33 
false33 
;33 
}44 
await66  
_addressesRepository66 &
.66& '
DeleteAddressAsync66' 9
(669 :
address66: A
)66A B
;66B C
return77 
true77 
;77 
}88 	
}99 
}:: ‡
g/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Managers/CategoriesManager.cs
	namespace 	
WebShopRestService
 
. 
Managers %
{ 
public 

class 
CategoriesManager "
{ 
private		 
readonly		 !
ICategoriesRepository		 .
_categoryRepository		/ B
;		B C
public 
CategoriesManager  
(  !!
ICategoriesRepository! 6
categoryRepository7 I
)I J
{ 	
_categoryRepository 
=  !
categoryRepository" 4
;4 5
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Category& .
>. /
>/ 0!
GetAllCategoriesAsync1 F
(F G
)G H
{ 	
return 
await 
_categoryRepository ,
., -!
GetAllCategoriesAsync- B
(B C
)C D
;D E
} 	
public 
async 
Task 
< 
Category "
>" # 
GetCategoryByIdAsync$ 8
(8 9
int9 <

categoryId= G
)G H
{ 	
return 
await 
_categoryRepository ,
., - 
GetCategoryByIdAsync- A
(A B

categoryIdB L
)L M
;M N
} 	
public 
async 
Task 
AddCategoryAsync *
(* +
Category+ 3
category4 <
)< =
{ 	
await 
_categoryRepository %
.% &
AddCategoryAsync& 6
(6 7
category7 ?
)? @
;@ A
} 	
public   
async   
Task   
UpdateCategoryAsync   -
(  - .
Category  . 6
category  7 ?
)  ? @
{!! 	
await## 
_categoryRepository## %
.##% &
UpdateCategoryAsync##& 9
(##9 :
category##: B
)##B C
;##C D
}$$ 	
public&& 
async&& 
Task&& 
DeleteCategoryAsync&& -
(&&- .
int&&. 1

categoryId&&2 <
)&&< =
{'' 	
await)) 
_categoryRepository)) %
.))% &
DeleteCategoryAsync))& 9
())9 :

categoryId)): D
)))D E
;))E F
}** 	
}00 
}11 ¶
f/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Managers/CustomersManager.cs
	namespace 	
WebShopRestService
 
. 
Managers %
{ 
public		 

class		 
CustomersManager		 !
{

 
private 
readonly  
ICustomersRepository - 
_customersRepository. B
;B C
public 
CustomersManager 
(   
ICustomersRepository  4
customersRepository5 H
)H I
{ 	 
_customersRepository  
=! "
customersRepository# 6
;6 7
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Customer& .
>. /
>/ 0
GetAll1 7
(7 8
)8 9
{ 	
return 
await  
_customersRepository -
.- . 
GetAllCustomersAsync. B
(B C
)C D
;D E
} 	
public 
async 
Task 
< 
Customer "
>" #
Get$ '
(' (
int( +
id, .
). /
{ 	
return 
await  
_customersRepository -
.- . 
GetCustomerByIdAsync. B
(B C
idC E
)E F
;F G
} 	
public 
async 
Task 
Update  
(  !
int! $
id% '
,' (
Customer) 1
customer2 :
): ;
{ 	
await  
_customersRepository &
.& '
UpdateCustomerAsync' :
(: ;
customer; C
)C D
;D E
} 	
public!! 
async!! 
Task!! 
<!! 
Customer!! "
>!!" #
Create!!$ *
(!!* +
Customer!!+ 3
customer!!4 <
)!!< =
{"" 	
await##  
_customersRepository## &
.##& '
AddCustomerAsync##' 7
(##7 8
customer##8 @
)##@ A
;##A B
return$$ 
customer$$ 
;$$ 
}%% 	
public'' 
async'' 
Task'' 
Delete''  
(''  !
int''! $
id''% '
)''' (
{(( 	
var)) 
customer)) 
=)) 
await))   
_customersRepository))! 5
.))5 6 
GetCustomerByIdAsync))6 J
())J K
id))K M
)))M N
;))N O
if** 
(** 
customer** 
!=** 
null**  
)**  !
{++ 
await,,  
_customersRepository,, *
.,,* +
DeleteCustomerAsync,,+ >
(,,> ?
customer,,? G
.,,G H

CustomerId,,H R
),,R S
;,,S T
}-- 
}.. 	
}// 
}00 ¯ 
g/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Managers/OrderItemsManager.cs
	namespace 	
WebShopRestService
 
. 
Managers %
{		 
public

 

class

 
OrderItemsManager

 "
{ 
private 
readonly 
IProductsRepository ,
_productRepository- ?
;? @
private 
readonly !
IOrderItemsRepository . 
_orderItemRepository/ C
;C D
public 
OrderItemsManager  
(  !
IProductsRepository! 4
productRepository5 F
,F G!
IOrderItemsRepositoryH ]
orderItemRepository^ q
)q r
{ 	
_productRepository 
=  
productRepository! 2
;2 3 
_orderItemRepository  
=! "
orderItemRepository# 6
;6 7
} 	
public 
async 
Task #
ValidateAndAddOrderItem 1
(1 2
	OrderItem2 ;
	orderItem< E
)E F
{ 	
var 
product 
= 
await 
_productRepository  2
.2 3
GetProductByIdAsync3 F
(F G
	orderItemG P
.P Q
	ProductIdQ Z
)Z [
;[ \
if 
( 
product 
== 
null 
)  
{ 
throw 
new %
InvalidOperationException 3
(3 4
$str4 M
)M N
;N O
} 
if 
( 
	orderItem 
. 
Price 
!=  "
product# *
.* +
Price+ 0
)0 1
{ 
throw 
new %
InvalidOperationException 3
(3 4
$str4 {
){ |
;| }
}   
await##  
_orderItemRepository## &
.##& '
AddOrderItemAsync##' 8
(##8 9
	orderItem##9 B
)##B C
;##C D
}$$ 	
public'' 
async'' 
Task'' 
AddOrderItemAsync'' +
(''+ ,
	OrderItem'', 5
	orderItem''6 ?
)''? @
{(( 	
await))  
_orderItemRepository)) &
.))& '
AddOrderItemAsync))' 8
())8 9
	orderItem))9 B
)))B C
;))C D
}** 	
public-- 
async-- 
Task-- 
<-- 
	OrderItem-- #
>--# $!
GetOrderItemByIdAsync--% :
(--: ;
int--; >
orderItemId--? J
)--J K
{.. 	
return// 
await//  
_orderItemRepository// -
.//- .!
GetOrderItemByIdAsync//. C
(//C D
orderItemId//D O
)//O P
;//P Q
}00 	
public33 
async33 
Task33  
UpdateOrderItemAsync33 .
(33. /
	OrderItem33/ 8
	orderItem339 B
)33B C
{44 	
await55  
_orderItemRepository55 &
.55& ' 
UpdateOrderItemAsync55' ;
(55; <
	orderItem55< E
)55E F
;55F G
}66 	
public99 
async99 
Task99  
DeleteOrderItemAsync99 .
(99. /
int99/ 2
orderItemId993 >
)99> ?
{:: 	
await;;  
_orderItemRepository;; &
.;;& ' 
DeleteOrderItemAsync;;' ;
(;;; <
orderItemId;;< G
);;G H
;;;H I
}<< 	
public?? 
async?? 
Task?? 
<?? 
IEnumerable?? %
<??% &
	OrderItem??& /
>??/ 0
>??0 1!
GetAllOrderItemsAsync??2 G
(??G H
)??H I
{@@ 	
returnAA 
awaitAA  
_orderItemRepositoryAA -
.AA- .!
GetAllOrderItemsAsyncAA. C
(AAC D
)AAD E
;AAE F
}BB 	
}CC 
}DD Œy
h/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Managers/OrderTablesManager.cs
	namespace 	
WebShopRestService
 
. 
Managers %
{		 
public 

class 
OrderTablesManager #
{ 
private 
readonly  
ICustomersRepository -
_customerRepository. A
;A B
private 
readonly  
IAddressesRepository -
_addressRepository. @
;@ A
private 
readonly "
IOrderTablesRepository /!
_orderTableRepository0 E
;E F
private 
readonly 
IProductsRepository ,
_productRepository- ?
;? @
public 
OrderTablesManager !
(! " 
ICustomersRepository" 6
customerRepository7 I
,I J 
IAddressesRepository! 5
addressRepository6 G
,G H"
IOrderTablesRepository! 7 
orderTableRepository8 L
,L M
IProductsRepository! 4
productRepository5 F
)F G
{ 	
_customerRepository   
=    !
customerRepository  " 4
??  5 7
throw  8 =
new  > A!
ArgumentNullException  B W
(  W X
nameof  X ^
(  ^ _
customerRepository  _ q
)  q r
)  r s
;  s t
_addressRepository!! 
=!!  
addressRepository!!! 2
??!!3 5
throw!!6 ;
new!!< ?!
ArgumentNullException!!@ U
(!!U V
nameof!!V \
(!!\ ]
addressRepository!!] n
)!!n o
)!!o p
;!!p q!
_orderTableRepository"" !
=""" # 
orderTableRepository""$ 8
??""9 ;
throw""< A
new""B E!
ArgumentNullException""F [
(""[ \
nameof""\ b
(""b c 
orderTableRepository""c w
)""w x
)""x y
;""y z
_productRepository## 
=##  
productRepository##! 2
??##3 5
throw##6 ;
new##< ?!
ArgumentNullException##@ U
(##U V
nameof##V \
(##\ ]
productRepository##] n
)##n o
)##o p
;##p q
}$$ 	
public'' 
async'' 
Task'' 
<'' 
IEnumerable'' %
<''% &

OrderTable''& 0
>''0 1
>''1 2
GetAllOrdersAsync''3 D
(''D E
)''E F
{(( 	
return** 
await** !
_orderTableRepository** .
.**. /
GetAllOrdersAsync**/ @
(**@ A
)**A B
;**B C
}++ 	
public-- 
async-- 
Task-- 
<-- 

OrderTable-- $
>--$ %
GetOrderByIdAsync--& 7
(--7 8
int--8 ;
orderId--< C
)--C D
{.. 	
var00 
order00 
=00 
await00 !
_orderTableRepository00 3
.003 4
GetOrderByIdAsync004 E
(00E F
orderId00F M
)00M N
;00N O
return22 
order22 
;22 
}33 	
public55 
async55 
Task55 
UpdateOrderAsync55 *
(55* +

OrderTable55+ 5
order556 ;
)55; <
{66 	
await88 !
_orderTableRepository88 '
.88' (
UpdateOrderAsync88( 8
(888 9
order889 >
)88> ?
;88? @
}:: 	
public<< 
async<< 
Task<< 
DeleteOrderAsync<< *
(<<* +
int<<+ .
orderId<</ 6
)<<6 7
{== 	
var?? 
order?? 
=?? 
await?? !
_orderTableRepository?? 3
.??3 4
GetOrderByIdAsync??4 E
(??E F
orderId??F M
)??M N
;??N O
if@@ 
(@@ 
order@@ 
==@@ 
null@@ 
)@@ 
{AA 
throwBB 
newBB %
InvalidOperationExceptionBB 3
(BB3 4
$strBB4 F
)BBF G
;BBG H
}CC 
awaitDD !
_orderTableRepositoryDD '
.DD' (
DeleteOrderAsyncDD( 8
(DD8 9
orderDD9 >
)DD> ?
;DD? @
}FF 	
publicLL 
asyncLL 
TaskLL $
ValidateAndAddOrderAsyncLL 2
(LL2 3

OrderTableLL3 =
orderLL> C
)LLC D
{MM 	
awaitNN "
ValidateCustomerExistsNN (
(NN( )
orderNN) .
.NN. /

CustomerIdNN/ 9
)NN9 :
;NN: ;
awaitOO !
ValidateAddressExistsOO '
(OO' (
orderOO( -
.OO- .
DeliveryAddressIdOO. ?
)OO? @
;OO@ A
ValidateOrderDatePP 
(PP 
orderPP #
.PP# $
	OrderDatePP$ -
)PP- .
;PP. /
awaitQQ '
ValidateDuplicateOrderAsyncQQ -
(QQ- .
orderQQ. 3
)QQ3 4
;QQ4 5
awaitRR 
ValidateOrderItemsRR $
(RR$ %
orderRR% *
.RR* +

OrderItemsRR+ 5
)RR5 6
;RR6 7
decimalTT 
calculatedTotalTT #
=TT$ %
CalculateOrderTotalTT& 9
(TT9 :
orderTT: ?
.TT? @

OrderItemsTT@ J
)TTJ K
;TTK L
ifUU 
(UU 
orderUU 
.UU 
TotalAmountUU !
!=UU" $
calculatedTotalUU% 4
)UU4 5
{VV 
throwWW 
newWW %
InvalidOperationExceptionWW 3
(WW3 4
$strWW4 i
)WWi j
;WWj k
}XX 
awaitZZ !
_orderTableRepositoryZZ '
.ZZ' (
AddOrderAsyncZZ( 5
(ZZ5 6
orderZZ6 ;
)ZZ; <
;ZZ< =
}[[ 	
privatebb 
asyncbb 
Taskbb "
ValidateCustomerExistsbb 1
(bb1 2
intbb2 5

customerIdbb6 @
)bb@ A
{cc 	
booldd 
existsdd 
=dd 
awaitdd 
_customerRepositorydd  3
.dd3 4
Existsdd4 :
(dd: ;

customerIddd; E
)ddE F
;ddF G
ifee 
(ee 
!ee 
existsee 
)ee 
{ff 
throwgg 
newgg %
InvalidOperationExceptiongg 3
(gg3 4
$strgg4 N
)ggN O
;ggO P
}hh 
}ii 	
privateoo 
asyncoo 
Taskoo !
ValidateAddressExistsoo 0
(oo0 1
intoo1 4
	addressIdoo5 >
)oo> ?
{pp 	
boolqq 
addressExistsqq 
=qq  
awaitqq! &
_addressRepositoryqq' 9
.qq9 :
AddressExistsAsyncqq: L
(qqL M
	addressIdqqM V
)qqV W
;qqW X
ifrr 
(rr 
!rr 
addressExistsrr 
)rr 
{ss 
throwtt 
newtt %
InvalidOperationExceptiontt 3
(tt3 4
$strtt4 V
)ttV W
;ttW X
}uu 
}vv 	
private|| 
void|| 
ValidateOrderDate|| &
(||& '
DateTime||' /
	orderDate||0 9
)||9 :
{}} 	
if~~ 
(~~ 
	orderDate~~ 
>~~ 
DateTime~~ $
.~~$ %
Now~~% (
)~~( )
{ 
throw
ÄÄ 
new
ÄÄ '
InvalidOperationException
ÄÄ 3
(
ÄÄ3 4
$str
ÄÄ4 Y
)
ÄÄY Z
;
ÄÄZ [
}
ÅÅ 
}
ÇÇ 	
private
àà 
async
àà 
Task
àà )
ValidateDuplicateOrderAsync
àà 6
(
àà6 7

OrderTable
àà7 A
order
ààB G
)
ààG H
{
ââ 	
var
ää 
	startTime
ää 
=
ää 
order
ää !
.
ää! "
	OrderDate
ää" +
.
ää+ ,

AddMinutes
ää, 6
(
ää6 7
-
ää7 8
$num
ää8 :
)
ää: ;
;
ää; <
var
ãã 
endTime
ãã 
=
ãã 
order
ãã 
.
ãã  
	OrderDate
ãã  )
.
ãã) *

AddMinutes
ãã* 4
(
ãã4 5
$num
ãã5 7
)
ãã7 8
;
ãã8 9
var
çç &
potentialDuplicateOrders
çç (
=
çç) *
await
çç+ 0#
_orderTableRepository
çç1 F
.
ççF G-
GetOrdersByCustomerAndDateAsync
ççG f
(
ççf g
order
ççg l
.
ççl m

CustomerId
ççm w
,
ççw x
	startTimeççy Ç
,ççÇ É
endTimeççÑ ã
)ççã å
;ççå ç
foreach
èè 
(
èè 
var
èè 
existingOrder
èè &
in
èè' )&
potentialDuplicateOrders
èè* B
)
èèB C
{
êê 
if
ëë 
(
ëë 
existingOrder
ëë !
.
ëë! "
OrderId
ëë" )
!=
ëë* ,
order
ëë- 2
.
ëë2 3
OrderId
ëë3 :
&&
ëë; =
DoOrderItemsMatch
ëë> O
(
ëëO P
existingOrder
ëëP ]
.
ëë] ^

OrderItems
ëë^ h
,
ëëh i
order
ëëj o
.
ëëo p

OrderItems
ëëp z
)
ëëz {
)
ëë{ |
{
íí 
throw
ìì 
new
ìì '
InvalidOperationException
ìì 7
(
ìì7 8
$str
ìì8 Y
)
ììY Z
;
ììZ [
}
îî 
}
ïï 
}
ññ 	
private
úú 
async
úú 
Task
úú  
ValidateOrderItems
úú -
(
úú- .
IEnumerable
úú. 9
<
úú9 :
	OrderItem
úú: C
>
úúC D

orderItems
úúE O
)
úúO P
{
ùù 	
if
ûû 
(
ûû 
!
ûû 

orderItems
ûû 
.
ûû 
Any
ûû 
(
ûû  
)
ûû  !
)
ûû! "
{
üü 
throw
†† 
new
†† '
InvalidOperationException
†† 3
(
††3 4
$str
††4 [
)
††[ \
;
††\ ]
}
°° 
foreach
££ 
(
££ 
var
££ 
item
££ 
in
££  

orderItems
££! +
)
££+ ,
{
§§ 
bool
¶¶ 
productExists
¶¶ "
=
¶¶# $
await
¶¶% * 
_productRepository
¶¶+ =
.
¶¶= > 
ProductExistsAsync
¶¶> P
(
¶¶P Q
item
¶¶Q U
.
¶¶U V
	ProductId
¶¶V _
)
¶¶_ `
;
¶¶` a
if
ßß 
(
ßß 
!
ßß 
productExists
ßß "
)
ßß" #
{
®® 
throw
©© 
new
©© '
InvalidOperationException
©© 7
(
©©7 8
$"
©©8 :
$str
©©: J
{
©©J K
item
©©K O
.
©©O P
	ProductId
©©P Y
}
©©Y Z
$str
©©Z j
"
©©j k
)
©©k l
;
©©l m
}
™™ 
if
¨¨ 
(
¨¨ 
item
¨¨ 
.
¨¨ 
Quantity
¨¨ !
<=
¨¨" $
$num
¨¨% &
)
¨¨& '
{
≠≠ 
throw
ÆÆ 
new
ÆÆ '
InvalidOperationException
ÆÆ 7
(
ÆÆ7 8
$str
ÆÆ8 _
)
ÆÆ_ `
;
ÆÆ` a
}
ØØ 
}
∞∞ 
}
±± 	
private
∏∏ 
decimal
∏∏ !
CalculateOrderTotal
∏∏ +
(
∏∏+ ,
IEnumerable
∏∏, 7
<
∏∏7 8
	OrderItem
∏∏8 A
>
∏∏A B

orderItems
∏∏C M
)
∏∏M N
{
ππ 	
return
∫∫ 

orderItems
∫∫ 
.
∫∫ 
Sum
∫∫ !
(
∫∫! "
item
∫∫" &
=>
∫∫' )
item
∫∫* .
.
∫∫. /
Quantity
∫∫/ 7
*
∫∫8 9
item
∫∫: >
.
∫∫> ?
Price
∫∫? D
)
∫∫D E
;
∫∫E F
}
ªª 	
private
ƒƒ 
bool
ƒƒ 
DoOrderItemsMatch
ƒƒ &
(
ƒƒ& '
IEnumerable
ƒƒ' 2
<
ƒƒ2 3
	OrderItem
ƒƒ3 <
>
ƒƒ< =
items1
ƒƒ> D
,
ƒƒD E
IEnumerable
ƒƒF Q
<
ƒƒQ R
	OrderItem
ƒƒR [
>
ƒƒ[ \
items2
ƒƒ] c
)
ƒƒc d
{
≈≈ 	
var
«« 
itemSet1
«« 
=
«« 
items1
«« !
.
««! "
OrderBy
««" )
(
««) *
i
««* +
=>
««, .
i
««/ 0
.
««0 1
	ProductId
««1 :
)
««: ;
.
««; <
ThenBy
««< B
(
««B C
i
««C D
=>
««E G
i
««H I
.
««I J
Quantity
««J R
)
««R S
;
««S T
var
»» 
itemSet2
»» 
=
»» 
items2
»» !
.
»»! "
OrderBy
»»" )
(
»») *
i
»»* +
=>
»», .
i
»»/ 0
.
»»0 1
	ProductId
»»1 :
)
»»: ;
.
»»; <
ThenBy
»»< B
(
»»B C
i
»»C D
=>
»»E G
i
»»H I
.
»»I J
Quantity
»»J R
)
»»R S
;
»»S T
return
ÀÀ 
itemSet1
ÀÀ 
.
ÀÀ 
SequenceEqual
ÀÀ )
(
ÀÀ) *
itemSet2
ÀÀ* 2
,
ÀÀ2 3
new
ÀÀ4 7
OrderItemComparer
ÀÀ8 I
(
ÀÀI J
)
ÀÀJ K
)
ÀÀK L
;
ÀÀL M
}
ÃÃ 	
private
—— 
class
—— 
OrderItemComparer
—— '
:
——( )
IEqualityComparer
——* ;
<
——; <
	OrderItem
——< E
>
——E F
{
““ 	
public
‘‘ 
bool
‘‘ 
Equals
‘‘ 
(
‘‘ 
	OrderItem
‘‘ (
x
‘‘) *
,
‘‘* +
	OrderItem
‘‘, 5
y
‘‘6 7
)
‘‘7 8
{
’’ 
return
÷÷ 
x
÷÷ 
.
÷÷ 
	ProductId
÷÷ "
==
÷÷# %
y
÷÷& '
.
÷÷' (
	ProductId
÷÷( 1
&&
÷÷2 4
x
÷÷5 6
.
÷÷6 7
Quantity
÷÷7 ?
==
÷÷@ B
y
÷÷C D
.
÷÷D E
Quantity
÷÷E M
;
÷÷M N
}
◊◊ 
public
‡‡ 
int
‡‡ 
GetHashCode
‡‡ "
(
‡‡" #
	OrderItem
‡‡# ,
obj
‡‡- 0
)
‡‡0 1
{
·· 
	unchecked
‚‚ 
{
„„ 
int
‰‰ 
hash
‰‰ 
=
‰‰ 
$num
‰‰ !
;
‰‰! "
hash
ÊÊ 
=
ÊÊ 
hash
ÊÊ 
*
ÊÊ  !
$num
ÊÊ" $
+
ÊÊ% &
obj
ÊÊ' *
.
ÊÊ* +
	ProductId
ÊÊ+ 4
.
ÊÊ4 5
GetHashCode
ÊÊ5 @
(
ÊÊ@ A
)
ÊÊA B
;
ÊÊB C
hash
ËË 
=
ËË 
hash
ËË 
*
ËË  !
$num
ËË" $
+
ËË% &
obj
ËË' *
.
ËË* +
Quantity
ËË+ 3
.
ËË3 4
GetHashCode
ËË4 ?
(
ËË? @
)
ËË@ A
;
ËËA B
return
ÈÈ 
hash
ÈÈ 
;
ÈÈ  
}
ÍÍ 
}
ÎÎ 
}
ÌÌ 	
}
ÔÔ 
} É
e/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Managers/ProductsManager.cs
	namespace 	
WebShopRestService
 
. 
Managers %
{ 
public		 

class		 
ProductsManager		  
{

 
private 
readonly 
IProductsRepository ,
_productsRepository- @
;@ A
public 
ProductsManager 
( 
IProductsRepository 2
productsRepository3 E
)E F
{ 	
_productsRepository 
=  !
productsRepository" 4
;4 5
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Product& -
>- .
>. /
GetAll0 6
(6 7
)7 8
{ 	
return 
await 
_productsRepository ,
., -
GetAllProductsAsync- @
(@ A
)A B
;B C
} 	
public 
async 
Task 
< 
Product !
>! "
Get# &
(& '
int' *
id+ -
)- .
{ 	
return 
await 
_productsRepository ,
., -
GetProductByIdAsync- @
(@ A
idA C
)C D
;D E
} 	
public 
async 
Task 
Update  
(  !
int! $
id% '
,' (
Product) 0
product1 8
)8 9
{ 	
await 
_productsRepository %
.% &
UpdateProductAsync& 8
(8 9
product9 @
)@ A
;A B
} 	
public!! 
async!! 
Task!! 
<!! 
Product!! !
>!!! "
Create!!# )
(!!) *
Product!!* 1
product!!2 9
)!!9 :
{"" 	
await## 
_productsRepository## %
.##% &
AddProductAsync##& 5
(##5 6
product##6 =
)##= >
;##> ?
return$$ 
product$$ 
;$$ 
}%% 	
public'' 
async'' 
Task'' 
Delete''  
(''  !
int''! $
id''% '
)''' (
{(( 	
var)) 
product)) 
=)) 
await)) 
_productsRepository))  3
.))3 4
GetProductByIdAsync))4 G
())G H
id))H J
)))J K
;))K L
if** 
(** 
product** 
!=** 
null** 
)**  
{++ 
await,, 
_productsRepository,, )
.,,) *
DeleteProductAsync,,* <
(,,< =
product,,= D
.,,D E
	ProductId,,E N
),,N O
;,,O P
}-- 
}.. 	
}00 
}11 ö
b/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Managers/RolesManager.cs
	namespace 	
WebShopRestService
 
. 
Managers %
{ 
public		 

class		 
RolesManager		 
{

 
private 
readonly 
IRolesRepository )
_rolesRepository* :
;: ;
public 
RolesManager 
( 
IRolesRepository ,
rolesRepository- <
)< =
{ 	
_rolesRepository 
= 
rolesRepository .
;. /
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Role& *
>* +
>+ ,
GetAll- 3
(3 4
)4 5
{ 	
return 
await 
_rolesRepository )
.) *
GetAllRolesAsync* :
(: ;
); <
;< =
} 	
public 
async 
Task 
< 
Role 
> 
Get  #
(# $
int$ '
id( *
)* +
{ 	
return 
await 
_rolesRepository )
.) *
GetRoleByIdAsync* :
(: ;
id; =
)= >
;> ?
} 	
public 
async 
Task 
Update  
(  !
int! $
id% '
,' (
Role) -
role. 2
)2 3
{ 	
await 
_rolesRepository "
." #
UpdateRoleAsync# 2
(2 3
role3 7
)7 8
;8 9
} 	
public!! 
async!! 
Task!! 
<!! 
Role!! 
>!! 
Create!!  &
(!!& '
Role!!' +
role!!, 0
)!!0 1
{"" 	
await## 
_rolesRepository## "
.##" #
AddRoleAsync### /
(##/ 0
role##0 4
)##4 5
;##5 6
return$$ 
role$$ 
;$$ 
}%% 	
public'' 
async'' 
Task'' 
Delete''  
(''  !
int''! $
id''% '
)''' (
{(( 	
var)) 
role)) 
=)) 
await)) 
_rolesRepository)) -
.))- .
GetRoleByIdAsync)). >
())> ?
id))? A
)))A B
;))B C
if** 
(** 
role** 
!=** 
null** 
)** 
{++ 
await-- 
_rolesRepository-- &
.--& '
DeleteRoleAsync--' 6
(--6 7
role--7 ;
.--; <
RoleId--< B
)--B C
;--C D
}.. 
}// 	
}11 
}22 —'
l/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Managers/UserCredentialsManager.cs
public 
class "
UserCredentialsManager #
{ 
private 
readonly 
	JwtConfig 

_jwtConfig )
;) *
private 
readonly 
MyDbContext  
_context! )
;) *
public 
"
UserCredentialsManager !
(! "
IOptions" *
<* +
	JwtConfig+ 4
>4 5
	jwtConfig6 ?
,? @
MyDbContextA L
contextM T
)T U
{ 

_jwtConfig 
= 
	jwtConfig 
. 
Value $
;$ %
_context 
= 
context 
; 
} 
public 

string 
HashPassword 
( 
string %
password& .
). /
{ 
return 
BCrypt 
. 
Net 
. 
BCrypt  
.  !
HashPassword! -
(- .
password. 6
)6 7
;7 8
} 
public   

bool   
VerifyPassword   
(   
string   %
password  & .
,  . /
string  0 6

storedHash  7 A
)  A B
{!! 
return"" 
BCrypt"" 
."" 
Net"" 
."" 
BCrypt""  
.""  !
Verify""! '
(""' (
password""( 0
,""0 1

storedHash""2 <
)""< =
;""= >
}## 
public%% 

string%% 
GenerateJwtToken%% "
(%%" #
UserCredential%%# 1
user%%2 6
)%%6 7
{&& 
var'' 
tokenHandler'' 
='' 
new'' #
JwtSecurityTokenHandler'' 6
(''6 7
)''7 8
;''8 9
var(( 
key(( 
=(( 
Encoding(( 
.(( 
ASCII((  
.((  !
GetBytes((! )
((() *

_jwtConfig((* 4
.((4 5
Secret((5 ;
)((; <
;((< =
var** 
userWithRole** 
=** 
_context** #
.**# $
UserCredentials**$ 3
.**3 4
Include**4 ;
(**; <
u**< =
=>**> @
u**A B
.**B C
Role**C G
)**G H
.**H I
SingleOrDefault**I X
(**X Y
u**Y Z
=>**[ ]
u**^ _
.**_ `
UserId**` f
==**g i
user**j n
.**n o
UserId**o u
)**u v
;**v w
var++ 
roleName++ 
=++ 
userWithRole++ #
?++# $
.++$ %
Role++% )
?++) *
.++* +
Name+++ /
??++0 2
string++3 9
.++9 :
Empty++: ?
;++? @
var-- 
claims-- 
=-- 
new-- 
List-- 
<-- 
Claim-- #
>--# $
{.. 	
new// 
Claim// 
(// 

ClaimTypes//  
.//  !
NameIdentifier//! /
,/// 0
user//1 5
.//5 6
UserId//6 <
.//< =
ToString//= E
(//E F
)//F G
)//G H
,//H I
new00 
Claim00 
(00 

ClaimTypes00  
.00  !
Name00! %
,00% &
user00' +
.00+ ,
Username00, 4
)004 5
,005 6
new11 
Claim11 
(11 

ClaimTypes11  
.11  !
Role11! %
,11% &
roleName11' /
)11/ 0
}22 	
;22	 

var44 
tokenDescriptor44 
=44 
new44 !#
SecurityTokenDescriptor44" 9
{55 	
Subject66 
=66 
new66 
ClaimsIdentity66 (
(66( )
claims66) /
)66/ 0
,660 1
Expires77 
=77 
DateTime77 
.77 
UtcNow77 %
.77% &
AddDays77& -
(77- .
$num77. /
)77/ 0
,770 1
SigningCredentials88 
=88  
new88! $
SigningCredentials88% 7
(887 8
new888 ; 
SymmetricSecurityKey88< P
(88P Q
key88Q T
)88T U
,88U V
SecurityAlgorithms88W i
.88i j
HmacSha256Signature88j }
)88} ~
}99 	
;99	 

var;; 
token;; 
=;; 
tokenHandler;;  
.;;  !
CreateToken;;! ,
(;;, -
tokenDescriptor;;- <
);;< =
;;;= >
return<< 
tokenHandler<< 
.<< 

WriteToken<< &
(<<& '
token<<' ,
)<<, -
;<<- .
}== 
}>> „
[/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/Address.cs
	namespace 	
WebShopRestService
 
. 
Models #
{ 
public 

class 
Address 
{ 
[ 	
Key	 
] 
public		 
int		 
	AddressId		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ;
); <
]< =
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* ]
)] ^
]^ _
[ 	
RegularExpression	 
( 
$str 5
,5 6
ErrorMessage7 C
=D E
$strF c
)c d
]d e
public 
string 
? 
Street 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 9
)9 :
]: ;
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage &
=' (
$str) Y
)Y Z
]Z [
[ 	
RegularExpression	 
( 
$str ,
,, -
ErrorMessage. :
=; <
$str= X
)X Y
]Y Z
public 
string 
? 
City 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ;
); <
]< =
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage &
=' (
$str) [
)[ \
]\ ]
[ 	
RegularExpression	 
( 
$str 0
,0 1
ErrorMessage2 >
=? @
$strA ^
)^ _
]_ `
public 
string 
? 

PostalCode !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 7
)7 8
]8 9
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage &
=' (
$str) \
)\ ]
]] ^
[ 	
RegularExpression	 
( 
$str ,
,, -
ErrorMessage. :
=; <
$str= [
)[ \
]\ ]
public 
string 
? 
Country 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
( 
ErrorMessage 
=  
$str! E
)E F
]F G
public   
ICollection   
<   
Customer   #
>  # $
?  $ %
	Customers  & /
{  0 1
get  2 5
;  5 6
set  7 :
;  : ;
}  < =
}!! 
}"" ≠
\/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/Category.cs
	namespace 	
WebShopRestService
 
. 
Models #
{ 
public 

class 
Category 
{ 
[		 	
Key			 
]		 
public

 
int

 

CategoryId

 
{

 
get

  #
;

# $
set

% (
;

( )
}

* +
[ 	
Required	 
( 
ErrorMessage 
=  
$str! =
)= >
]> ?
[ 	
StringLength	 
( 
$num 
, 
MinimumLength (
=) *
$num+ ,
,, -
ErrorMessage. :
=; <
$str= w
)w x
]x y
[ 	
RegularExpression	 
( 
$str 1
,1 2
ErrorMessage3 ?
=@ A
$str	B ¶
)
¶ ß
]
ß ®
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* U
)U V
]V W
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
	NotMapped	 
] 
public 
ICollection 
< 
Product "
>" #
Products$ ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
} 	
} ÷"
\/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/Customer.cs
public 
class 
Customer 
{ 
[ 
Key 
] 	
public		 

int		 

CustomerId		 
{		 
get		 
;		  
set		! $
;		$ %
}		& '
[ 
Required 
( 
ErrorMessage 
= 
$str 6
)6 7
]7 8
[ 
StringLength 
( 
$num 
, 
ErrorMessage "
=# $
$str% V
)V W
]W X
[ 
RegularExpression 
( 
$str (
,( )
ErrorMessage* 6
=7 8
$str9 U
)U V
]V W
public 

string 
	FirstName 
{ 
get !
;! "
set# &
;& '
}( )
[ 
Required 
( 
ErrorMessage 
= 
$str 5
)5 6
]6 7
[ 
StringLength 
( 
$num 
, 
ErrorMessage "
=# $
$str% U
)U V
]V W
[ 
RegularExpression 
( 
$str (
,( )
ErrorMessage* 6
=7 8
$str9 T
)T U
]U V
public 

string 
LastName 
{ 
get  
;  !
set" %
;% &
}' (
[ 
Required 
( 
ErrorMessage 
= 
$str 1
)1 2
]2 3
[ 
EmailAddress 
( 
ErrorMessage 
=  
$str! 8
)8 9
]9 :
[ 
StringLength 
( 
$num 
, 
ErrorMessage #
=$ %
$str& S
)S T
]T U
[ 
RegularExpression 
( 
$str >
,> ?
ErrorMessage@ L
=M N
$strO f
)f g
]g h
public 

string 
Email 
{ 
get 
; 
set "
;" #
}$ %
[ 
Required 
( 
ErrorMessage 
= 
$str 8
)8 9
]9 :
[ 
Phone 

(
 
ErrorMessage 
= 
$str 8
)8 9
]9 :
[ 
StringLength 
( 
$num 
, 
ErrorMessage "
=# $
$str% X
)X Y
]Y Z
public 

string 
Phone 
{ 
get 
; 
set "
;" #
}$ %
[   
Required   
(   
ErrorMessage   
=   
$str   6
)  6 7
]  7 8
[!! 

ForeignKey!! 
(!! 
$str!! 
)!! 
]!! 
public"" 

int"" 
	AddressId"" 
{"" 
get"" 
;"" 
set""  #
;""# $
}""% &
public## 

Address## 
Address## 
{## 
get##  
;##  !
set##" %
;##% &
}##' (
[%% 
Required%% 
(%% 
ErrorMessage%% 
=%% 
$str%% 3
)%%3 4
]%%4 5
[&& 

ForeignKey&& 
(&& 
$str&&  
)&&  !
]&&! "
public'' 

int'' 
UserId'' 
{'' 
get'' 
;'' 
set''  
;''  !
}''" #
public(( 

UserCredential(( 
UserCredential(( (
{(() *
get((+ .
;((. /
set((0 3
;((3 4
}((5 6
public** 

ICollection** 
<** 

OrderTable** !
>**! "
Orders**# )
{*** +
get**, /
;**/ 0
set**1 4
;**4 5
}**6 7
}++ ø
]/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/OrderItem.cs
	namespace 	
WebShopRestService
 
. 
Models #
{ 
public 

class 
	OrderItem 
{ 
[ 	
Key	 
] 
public		 
int		 
OrderItemId		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 8
)8 9
]9 :
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ M
)M N
]N O
public 
int 
Quantity 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 5
)5 6
]6 7
[ 	
Range	 
( 
$num 
, 
$num 
,  
ErrorMessage! -
=. /
$str0 ^
)^ _
]_ `
[ 	
DataType	 
( 
DataType 
. 
Currency #
,# $
ErrorMessage% 1
=2 3
$str4 K
)K L
]L M
[ 	
Column	 
( 
TypeName 
= 
$str +
)+ ,
], -
public 
decimal 
Price 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 8
)8 9
]9 :
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ T
)T U
]U V
[ 	

ForeignKey	 
( 
$str  
)  !
]! "
public 
int 
OrderId 
{ 
get  
;  !
set" %
;% &
}' (
public 
virtual 

OrderTable !

OrderTable" ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
[ 	
Required	 
( 
ErrorMessage 
=  
$str! :
): ;
]; <
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ V
)V W
]W X
[ 	

ForeignKey	 
( 
$str 
) 
] 
public 
int 
	ProductId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
virtual 
Product 
Product &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
}## 
}$$ ÷
^/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/OrderTable.cs
	namespace 	
WebShopRestService
 
. 
Models #
{ 
public 

class 

OrderTable 
{		 
[

 	
Key

	 
]

 
public 
int 
OrderId 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Required	 
( 
ErrorMessage 
=  
$str! :
): ;
]; <
public 
DateTime 
	OrderDate !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
Column	 
( 
TypeName 
= 
$str *
)* +
]+ ,
[ 	
Range	 
( 
$num 
, 
$num  
,  !
ErrorMessage" .
=/ 0
$str1 g
)g h
]h i
public 
decimal 
TotalAmount "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ;
); <
]< =
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ W
)W X
]X Y
[ 	

ForeignKey	 
( 
$str 
) 
]  
public 
int 

CustomerId 
{ 
get  #
;# $
set% (
;( )
}* +
public 
Customer 
Customer  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 	
Required	 
( 
ErrorMessage 
=  
$str! C
)C D
]D E
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ _
)_ `
]` a
[ 	

ForeignKey	 
( 
$str %
)% &
]& '
public 
int 
DeliveryAddressId $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
Address 
DeliveryAddress &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public!! 
ICollection!! 
<!! 
	OrderItem!! $
>!!$ %

OrderItems!!& 0
{!!1 2
get!!3 6
;!!6 7
set!!8 ;
;!!; <
}!!= >
public## 

OrderTable## 
(## 
)## 
{$$ 	

OrderItems%% 
=%% 
new%% 
HashSet%% $
<%%$ %
	OrderItem%%% .
>%%. /
(%%/ 0
)%%0 1
;%%1 2
}&& 	
}'' 
}(( ÿ
[/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/Payment.cs
	namespace 	
WebShopRestService
 
. 
Models #
{ 
public 

class 
Payment 
{ 
[		 	
Key			 
]		 
public

 
int

 
	PaymentId

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
[ 	
Required	 
( 
ErrorMessage 
=  
$str! >
)> ?
]? @
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* `
)` a
]a b
public 
string 
PaymentMethod #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
DataType	 
( 
DataType 
. 
Date 
)  
]  !
public 
DateTime 
PaymentDate #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 6
)6 7
]7 8
[ 	
Column	 
( 
TypeName 
= 
$str +
)+ ,
], -
[ 	
Range	 
( 
$num 
, 
double 
. 
MaxValue $
,$ %
ErrorMessage& 2
=3 4
$str5 X
)X Y
]Y Z
public 
decimal 
Amount 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 8
)8 9
]9 :
[ 	

ForeignKey	 
( 
$str  
)  !
]! "
public 
int 
OrderId 
{ 
get  
;  !
set" %
;% &
}' (
public 

OrderTable 

OrderTable $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} ±(
`/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/PaymentAudit.cs
	namespace 	
WebShopRestService
 
. 
Models #
{ 
public 

class 
PaymentAudit 
{ 
[		 	
Key			 
]		 
public

 
int

 
PaymentAuditId

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
[ 	

ForeignKey	 
( 
$str  
)  !
]! "
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 8
)8 9
]9 :
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ T
)T U
]U V
public 
int 
OrderId 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 4
)4 5
]5 6
[ 	
DataType	 
( 
DataType 
. 
Date 
)  
]  !
[ 	
DateNotInTheFuture	 
( 
ErrorMessage (
=) *
$str+ J
)J K
]K L
public 
DateTime 
Date 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 6
)6 7
]7 8
[ 	
Column	 
( 
TypeName 
= 
$str *
)* +
]+ ,
[ 	
Range	 
( 
$num 
, 
$num 
,  
ErrorMessage! -
=. /
$str0 \
)\ ]
]] ^
[ 	
NonNegativeValue	 
( 
ErrorMessage &
=' (
$str) E
)E F
]F G
public 
decimal 
Amount 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ;
); <
]< =
[   	
StringLength  	 
(   
$num   
,   
ErrorMessage   &
=  ' (
$str  ) [
)  [ \
]  \ ]
["" 	
RegularExpression""	 
("" 
$str"" )
,"") *
ErrorMessage""+ 7
=""8 9
$str"": b
)""b c
]""c d
public## 
string## 

ActionType##  
{##! "
get### &
;##& '
set##( +
;##+ ,
}##- .
[%% 	
Required%%	 
(%% 
ErrorMessage%% 
=%%  
$str%%! ;
)%%; <
]%%< =
[&& 	
DataType&&	 
(&& 
DataType&& 
.&& 
DateTime&& #
)&&# $
]&&$ %
[(( 	
DateNotInTheFuture((	 
((( 
ErrorMessage(( (
=(() *
$str((+ Q
)((Q R
]((R S
public)) 
DateTime)) 

ActionDate)) "
{))# $
get))% (
;))( )
set))* -
;))- .
}))/ 0
public++ 

OrderTable++ 

OrderTable++ $
{++% &
get++' *
;++* +
set++, /
;++/ 0
}++1 2
},, 
public// 

class// '
DateNotInTheFutureAttribute// ,
://- .
ValidationAttribute/// B
{00 
public11 
override11 
bool11 
IsValid11 $
(11$ %
object11% +
value11, 1
)111 2
{22 	
var33 
date33 
=33 
(33 
DateTime33  
)33  !
value33! &
;33& '
return44 
date44 
<=44 
DateTime44 #
.44# $
Now44$ '
;44' (
}55 	
}66 
public99 

class99 %
NonNegativeValueAttribute99 *
:99+ ,
ValidationAttribute99- @
{:: 
public;; 
override;; 
bool;; 
IsValid;; $
(;;$ %
object;;% +
value;;, 1
);;1 2
{<< 	
if== 
(== 
value== 
is== 
decimal==  
decimalValue==! -
)==- .
{>> 
return?? 
decimalValue?? #
>=??$ &
$num??' (
;??( )
}@@ 
returnAA 
trueAA 
;AA 
}BB 	
}CC 
}DD ° 
[/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/Product.cs
	namespace 	
WebShopRestService
 
. 
Models #
{ 
public 

class 
Product 
{		 
[

 	
Key

	 
]

 
public 
int 
	ProductId 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* ^
)^ _
]_ `
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* U
)U V
]V W
[ 	
DataType	 
( 
DataType 
. 
MultilineText (
)( )
]) *
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
StringLength	 
( 
$num 
) 
] 
public 
string 
Img 
{ 
get 
;  
set! $
;$ %
}& '
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 5
)5 6
]6 7
[ 	
Column	 
( 
TypeName 
= 
$str +
)+ ,
], -
[ 	
Range	 
( 
$num 
, 
$num 
,  
ErrorMessage! -
=. /
$str0 [
)[ \
]\ ]
public 
decimal 
Price 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Required	 
( 
ErrorMessage 
=  
$str! >
)> ?
]? @
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ S
)S T
]T U
public 
int 
StockQuantity  
{! "
get# &
;& '
set( +
;+ ,
}- .
[!! 	
Required!!	 
(!! 
ErrorMessage!! 
=!!  
$str!!! ;
)!!; <
]!!< =
["" 	
Range""	 
("" 
$num"" 
,"" 
int"" 
."" 
MaxValue"" 
,"" 
ErrorMessage""  ,
=""- .
$str""/ W
)""W X
]""X Y
[## 	

ForeignKey##	 
(## 
$str## 
)## 
]##  
public$$ 
int$$ 

CategoryId$$ 
{$$ 
get$$  #
;$$# $
set$$% (
;$$( )
}$$* +
}%% 
public'' 

class'' 
ProductExtra'' 
:'' 
Product''  '
{(( 
[)) 	
	AllowNull))	 
])) 
public** 
Category** 
Category**  
{**! "
get**# &
;**& '
set**( +
;**+ ,
}**- .
[++ 	
	AllowNull++	 
]++ 
public-- 
ICollection-- 
<-- 
	OrderItem-- $
>--$ %

OrderItems--& 0
{--1 2
get--3 6
;--6 7
set--8 ;
;--; <
}--= >
}.. 
}// ƒ
`/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/ProductAudit.cs
	namespace 	
WebShopRestService
 
. 
Models #
{ 
public 

class 
ProductAudit 
{ 
[		 	
Key			 
]		 
public

 
int

 
AuditId

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
[ 	
Column	 
( 
TypeName 
= 
$str +
)+ ,
], -
[ 	
Range	 
( 
$num 
, 
$num 
, 
ErrorMessage *
=+ ,
$str- Y
)Y Z
]Z [
public 
decimal 
OldPrice 
{  !
get" %
;% &
set' *
;* +
}, -
[ 	
Column	 
( 
TypeName 
= 
$str +
)+ ,
], -
[ 	
Range	 
( 
$num 
, 
$num 
, 
ErrorMessage *
=+ ,
$str- Y
)Y Z
]Z [
public 
decimal 
NewPrice 
{  !
get" %
;% &
set' *
;* +
}, -
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ;
); <
]< =
[ 	
DataType	 
( 
DataType 
. 
DateTime #
)# $
]$ %
[ 	
DateNotInTheFuture	 
( 
ErrorMessage (
=) *
$str+ Q
)Q R
]R S
public 
DateTime 

ChangeDate "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
Required	 
( 
ErrorMessage 
=  
$str! :
): ;
]; <
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ V
)V W
]W X
[ 	

ForeignKey	 
( 
$str 
) 
] 
public 
int 
	ProductId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
Product 
Product 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} ˇ
X/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/Role.cs
	namespace 	
WebShopRestService
 
. 
Models #
{ 
public 

class 
Role 
{ 
[ 	
Key	 
] 
public		 
int		 
RoleId		 
{		 
get		 
;		  
set		! $
;		$ %
}		& '
public

 
string

 
Name

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
public 
int 
AccessLevel 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
ICollection 
< 
UserCredential )
>) *
UserCredentials+ :
{; <
get= @
;@ A
setB E
;E F
}G H
} 
} ∞
b/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Models/UserCredential.cs
	namespace 	
WebShopRestService
 
. 
Models #
{ 
public 

class 
UserCredential 
{ 
[		 	
Key			 
]		 
public

 
int

 
UserId

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
[ 	
Required	 
( 
ErrorMessage 
=  
$str! B
)B C
]C D
[ 	
EmailAddress	 
( 
ErrorMessage "
=# $
$str% <
)< =
]= >
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* W
)W X
]X Y
public 
string 
Username 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 8
)8 9
]9 :
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* Z
)Z [
][ \
public 
string 
HashedPassword $
{% &
get' *
;* +
set, /
;/ 0
}1 2
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 7
)7 8
]8 9
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ S
)S T
]T U
[ 	

ForeignKey	 
( 
$str 
) 
] 
public 
int 
RoleId 
{ 
get 
;  
set! $
;$ %
}& '
public 
Role 
Role 
{ 
get 
; 
set  #
;# $
}% &
public 
ICollection 
< 
Customer #
># $
	Customers% .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
} 
} ì*
T/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Program.cs
var		 
builder		 
=		 
WebApplication		 
.		 
CreateBuilder		 *
(		* +
args		+ /
)		/ 0
;		0 1
var 
jwtConfigSection 
= 
builder 
. 
Configuration ,
., -

GetSection- 7
(7 8
$str8 C
)C D
;D E
builder 
. 
Services 
. 
	Configure 
< 
	JwtConfig $
>$ %
(% &
jwtConfigSection& 6
)6 7
;7 8
var 
	jwtConfig 
= 
jwtConfigSection  
.  !
Get! $
<$ %
	JwtConfig% .
>. /
(/ 0
)0 1
;1 2
if 
( 
string 

.
 
IsNullOrEmpty 
( 
	jwtConfig "
." #
Secret# )
)) *
)* +
{ 
throw 	
new
 %
InvalidOperationException '
(' (
$str( M
)M N
;N O
} 
builder 
. 
Services 
. 
AddControllers 
(  
)  !
;! "
builder 
. 
Services 
. 
AddDbContext 
< 
MyDbContext )
>) *
(* +
options+ 2
=>3 5
options 
. 
UseSqlServer 
( 
builder  
.  !
Configuration! .
.. /
GetConnectionString/ B
(B C
$strC S
)S T
)T U
)U V
;V W
builder 
. 
Services 
. 
AddAutoMapper 
( 
typeof %
(% &
MapperInitializer& 7
)7 8
)8 9
;9 :
builder 
. 
Services 
. 
	AddScoped 
< "
UserCredentialsManager 1
>1 2
(2 3
)3 4
;4 5
builder!! 
.!! 
Services!! 
.!! 
AddAuthentication!! "
(!!" #
JwtBearerDefaults!!# 4
.!!4 5 
AuthenticationScheme!!5 I
)!!I J
."" 
AddJwtBearer"" 
("" 
options"" 
=>"" 
{## 
options$$ 
.$$ %
TokenValidationParameters$$ )
=$$* +
new$$, /%
TokenValidationParameters$$0 I
{%% 	$
ValidateIssuerSigningKey'' $
=''% &
true''' +
,''+ ,
IssuerSigningKey)) 
=)) 
new)) " 
SymmetricSecurityKey))# 7
())7 8
Encoding))8 @
.))@ A
UTF8))A E
.))E F
GetBytes))F N
())N O
	jwtConfig))O X
.))X Y
Secret))Y _
)))_ `
)))` a
,))a b
ValidateIssuer,, 
=,, 
false,, "
,,," #
ValidateAudience-- 
=-- 
false-- $
,--$ %
	ClockSkew00 
=00 
TimeSpan00  
.00  !
Zero00! %
}11 	
;11	 

}22 
)22 
;22 
builder66 
.66 
Services66 
.66 #
AddEndpointsApiExplorer66 (
(66( )
)66) *
;66* +
builder77 
.77 
Services77 
.77 
AddSwaggerGen77 
(77 
)77  
;77  !
var99 
app99 
=99 	
builder99
 
.99 
Build99 
(99 
)99 
;99 
if<< 
(<< 
app<< 
.<< 
Environment<< 
.<< 
IsDevelopment<< !
(<<! "
)<<" #
)<<# $
{== 
app?? 
.?? %
UseDeveloperExceptionPage?? !
(??! "
)??" #
;??# $
appAA 
.AA 

UseSwaggerAA 
(AA 
)AA 
;AA 
appCC 
.CC 
UseSwaggerUICC 
(CC 
)CC 
;CC 
}DD 
appFF 
.FF 
UseCorsFF 
(FF 
xFF 
=>FF 
xFF 
.FF 
AllowAnyOriginFF !
(FF! "
)FF" #
.FF# $
AllowAnyMethodFF$ 2
(FF2 3
)FF3 4
.FF4 5
AllowAnyHeaderFF5 C
(FFC D
)FFD E
)FFE F
;FFF G
appII 
.II 
UseHttpsRedirectionII 
(II 
)II 
;II 
appLL 
.LL 
UseAuthenticationLL 
(LL 
)LL 
;LL 
appMM 
.MM 
UseAuthorizationMM 
(MM 
)MM 
;MM 
appPP 
.PP 
MapControllersPP 
(PP 
)PP 
;PP 
appSS 
.SS 
RunSS 
(SS 
)SS 	
;SS	 
Œ
m/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Repositories/AddressesRepository.cs
	namespace		 	
WebShopRestService		
 
.		 
Repositories		 )
{

 
public 

class 
AddressesRepository $
:% & 
IAddressesRepository' ;
{ 
private 
readonly 
MyDbContext $
_context% -
;- .
public 
AddressesRepository "
(" #
MyDbContext# .
context/ 6
)6 7
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Address& -
>- .
>. / 
GetAllAddressesAsync0 D
(D E
)E F
{ 	
return 
await 
_context !
.! "
	Addresses" +
.+ ,
ToListAsync, 7
(7 8
)8 9
;9 :
} 	
public 
async 
Task 
< 
Address !
>! "
GetAddressByIdAsync# 6
(6 7
int7 :
	addressId; D
)D E
{ 	
return 
await 
_context !
.! "
	Addresses" +
.+ ,
	FindAsync, 5
(5 6
	addressId6 ?
)? @
;@ A
} 	
public 
async 
Task 
AddAddressAsync )
() *
Address* 1
address2 9
)9 :
{ 	
_context   
.   
	Addresses   
.   
Add   "
(  " #
address  # *
)  * +
;  + ,
await!! 
_context!! 
.!! 
SaveChangesAsync!! +
(!!+ ,
)!!, -
;!!- .
}"" 	
public$$ 
async$$ 
Task$$ 
UpdateAddressAsync$$ ,
($$, -
Address$$- 4
address$$5 <
)$$< =
{%% 	
_context&& 
.&& 
Entry&& 
(&& 
address&& "
)&&" #
.&&# $
State&&$ )
=&&* +
EntityState&&, 7
.&&7 8
Modified&&8 @
;&&@ A
await'' 
_context'' 
.'' 
SaveChangesAsync'' +
(''+ ,
)'', -
;''- .
}(( 	
public** 
async** 
Task** 
DeleteAddressAsync** ,
(**, -
Address**- 4
address**5 <
)**< =
{++ 	
_context,, 
.,, 
	Addresses,, 
.,, 
Remove,, %
(,,% &
address,,& -
),,- .
;,,. /
await-- 
_context-- 
.-- 
SaveChangesAsync-- +
(--+ ,
)--, -
;--- .
}.. 	
public00 
async00 
Task00 
<00 
bool00 
>00 
AddressExistsAsync00  2
(002 3
int003 6
	addressId007 @
)00@ A
{11 	
return22 
await22 
_context22 !
.22! "
	Addresses22" +
.22+ ,
AnyAsync22, 4
(224 5
e225 6
=>227 9
e22: ;
.22; <
	AddressId22< E
==22F H
	addressId22I R
)22R S
;22S T
}33 	
}44 
}55 ø
n/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Repositories/CategoriesRepository.cs
public 
class  
CategoriesRepository !
:" #!
ICategoriesRepository$ 9
{ 
private 
readonly 
MyDbContext  
_context! )
;) *
public

 
 
CategoriesRepository

 
(

  
MyDbContext

  +
context

, 3
)

3 4
{ 
_context 
= 
context 
; 
} 
public 

async 
Task 
< 
IEnumerable !
<! "
Category" *
>* +
>+ ,!
GetAllCategoriesAsync- B
(B C
)C D
{ 
return 
await 
_context 
. 

Categories (
.( )
ToListAsync) 4
(4 5
)5 6
;6 7
} 
public 

async 
Task 
< 
Category 
>  
GetCategoryByIdAsync  4
(4 5
int5 8

categoryId9 C
)C D
{ 
return 
await 
_context 
. 

Categories (
.( )
	FindAsync) 2
(2 3

categoryId3 =
)= >
;> ?
} 
public 

async 
Task 
AddCategoryAsync &
(& '
Category' /
category0 8
)8 9
{ 
_context 
. 

Categories 
. 
Add 
(  
category  (
)( )
;) *
await 
_context 
. 
SaveChangesAsync '
(' (
)( )
;) *
} 
public 

async 
Task 
UpdateCategoryAsync )
() *
Category* 2
category3 ;
); <
{   
_context!! 
.!! 
Entry!! 
(!! 
category!! 
)!!  
.!!  !
State!!! &
=!!' (
EntityState!!) 4
.!!4 5
Modified!!5 =
;!!= >
await"" 
_context"" 
."" 
SaveChangesAsync"" '
(""' (
)""( )
;"") *
}## 
public%% 

async%% 
Task%% 
DeleteCategoryAsync%% )
(%%) *
int%%* -

categoryId%%. 8
)%%8 9
{&& 
var'' 
category'' 
='' 
await'' 
_context'' %
.''% &

Categories''& 0
.''0 1
	FindAsync''1 :
('': ;

categoryId''; E
)''E F
;''F G
if(( 

((( 
category(( 
!=(( 
null(( 
)(( 
{)) 	
_context** 
.** 

Categories** 
.**  
Remove**  &
(**& '
category**' /
)**/ 0
;**0 1
await++ 
_context++ 
.++ 
SaveChangesAsync++ +
(+++ ,
)++, -
;++- .
},, 	
}-- 
}.. «
m/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Repositories/CustomersRepository.cs
	namespace 	
WebShopRestService
 
. 
Repositories )
{ 
public 

class 
CustomersRepository $
:% & 
ICustomersRepository' ;
{ 
private		 
readonly		 
MyDbContext		 $
_context		% -
;		- .
public 
CustomersRepository "
(" #
MyDbContext# .
context/ 6
)6 7
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Customer& .
>. /
>/ 0 
GetAllCustomersAsync1 E
(E F
)F G
{ 	
return 
await 
_context !
.! "
	Customers" +
.+ ,
ToListAsync, 7
(7 8
)8 9
;9 :
} 	
public 
async 
Task 
< 
Customer "
>" # 
GetCustomerByIdAsync$ 8
(8 9
int9 <

customerId= G
)G H
{ 	
return 
await 
_context !
.! "
	Customers" +
.+ ,
	FindAsync, 5
(5 6

customerId6 @
)@ A
;A B
} 	
public 
async 
Task 
AddCustomerAsync *
(* +
Customer+ 3
customer4 <
)< =
{ 	
_context 
. 
	Customers 
. 
Add "
(" #
customer# +
)+ ,
;, -
await 
_context 
. 
SaveChangesAsync +
(+ ,
), -
;- .
} 	
public   
async   
Task   
UpdateCustomerAsync   -
(  - .
Customer  . 6
customer  7 ?
)  ? @
{!! 	
_context"" 
."" 
Entry"" 
("" 
customer"" #
)""# $
.""$ %
State""% *
=""+ ,
EntityState""- 8
.""8 9
Modified""9 A
;""A B
await## 
_context## 
.## 
SaveChangesAsync## +
(##+ ,
)##, -
;##- .
}$$ 	
public%% 
async%% 
Task%% 
<%% 
bool%% 
>%% 
Exists%%  &
(%%& '
int%%' *

customerId%%+ 5
)%%5 6
{&& 	
return'' 
await'' 
_context'' !
.''! "
	Customers''" +
.''+ ,
AnyAsync'', 4
(''4 5
c''5 6
=>''7 9
c'': ;
.''; <

CustomerId''< F
==''G I

customerId''J T
)''T U
;''U V
}(( 	
public** 
async** 
Task** 
DeleteCustomerAsync** -
(**- .
int**. 1

customerId**2 <
)**< =
{++ 	
var,, 
customer,, 
=,, 
await,,  
_context,,! )
.,,) *
	Customers,,* 3
.,,3 4
	FindAsync,,4 =
(,,= >

customerId,,> H
),,H I
;,,I J
if-- 
(-- 
customer-- 
!=-- 
null--  
)--  !
{.. 
_context// 
.// 
	Customers// "
.//" #
Remove//# )
(//) *
customer//* 2
)//2 3
;//3 4
await00 
_context00 
.00 
SaveChangesAsync00 /
(00/ 0
)000 1
;001 2
}11 
}22 	
}33 
}44 “
n/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Repositories/OrderItemsRepository.cs
public 
class  
OrderItemsRepository !
:" #!
IOrderItemsRepository$ 9
{		 
private

 
readonly

 
MyDbContext

  
_context

! )
;

) *
public 
 
OrderItemsRepository 
(  
MyDbContext  +
context, 3
)3 4
{ 
_context 
= 
context 
; 
} 
public 

async 
Task 
< 
IEnumerable !
<! "
	OrderItem" +
>+ ,
>, -!
GetAllOrderItemsAsync. C
(C D
)D E
{ 
return 
await 
_context 
. 

OrderItems (
.( )
ToListAsync) 4
(4 5
)5 6
;6 7
} 
public 

async 
Task 
< 
	OrderItem 
>  !
GetOrderItemByIdAsync! 6
(6 7
int7 :
orderItemId; F
)F G
{ 
return 
await 
_context 
. 

OrderItems (
.( )
	FindAsync) 2
(2 3
orderItemId3 >
)> ?
;? @
} 
public 

async 
Task 
AddOrderItemAsync '
(' (
	OrderItem( 1
	orderItem2 ;
); <
{ 
_context 
. 

OrderItems 
. 
Add 
(  
	orderItem  )
)) *
;* +
await 
_context 
. 
SaveChangesAsync '
(' (
)( )
;) *
} 
public!! 

async!! 
Task!!  
UpdateOrderItemAsync!! *
(!!* +
	OrderItem!!+ 4
	orderItem!!5 >
)!!> ?
{"" 
_context## 
.## 
Entry## 
(## 
	orderItem##  
)##  !
.##! "
State##" '
=##( )
EntityState##* 5
.##5 6
Modified##6 >
;##> ?
await$$ 
_context$$ 
.$$ 
SaveChangesAsync$$ '
($$' (
)$$( )
;$$) *
}%% 
public'' 

async'' 
Task''  
DeleteOrderItemAsync'' *
(''* +
int''+ .
orderItemId''/ :
)'': ;
{(( 
var)) 
	orderItem)) 
=)) 
await)) 
_context)) &
.))& '

OrderItems))' 1
.))1 2
	FindAsync))2 ;
()); <
orderItemId))< G
)))G H
;))H I
if** 

(** 
	orderItem** 
!=** 
null** 
)** 
{++ 	
_context,, 
.,, 

OrderItems,, 
.,,  
Remove,,  &
(,,& '
	orderItem,,' 0
),,0 1
;,,1 2
await-- 
_context-- 
.-- 
SaveChangesAsync-- +
(--+ ,
)--, -
;--- .
}.. 	
}// 
}00 Ï%
o/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Repositories/OrderTablesRepository.cs
public

 
class

 !
OrderTablesRepository

 "
:

# $"
IOrderTablesRepository

% ;
{ 
private 
readonly 
MyDbContext  
_context! )
;) *
public 
!
OrderTablesRepository  
(  !
MyDbContext! ,
context- 4
)4 5
{ 
_context 
= 
context 
; 
} 
public 

async 
Task 
< 
IEnumerable !
<! "

OrderTable" ,
>, -
>- .
GetAllOrdersAsync/ @
(@ A
)A B
{ 
return 
await 
_context 
. 
OrderTables )
.) *
ToListAsync* 5
(5 6
)6 7
;7 8
} 
public 

async 
Task 
< 

OrderTable  
>  !
GetOrderByIdAsync" 3
(3 4
int4 7
orderId8 ?
)? @
{ 
return 
await 
_context 
. 
OrderTables )
.) *
	FindAsync* 3
(3 4
orderId4 ;
); <
;< =
} 
public 

async 
Task 
AddOrderAsync #
(# $

OrderTable$ .
order/ 4
)4 5
{ 
_context 
. 
OrderTables 
. 
Add  
(  !
order! &
)& '
;' (
await   
_context   
.   
SaveChangesAsync   '
(  ' (
)  ( )
;  ) *
}!! 
public## 

async## 
Task## 
UpdateOrderAsync## &
(##& '

OrderTable##' 1
order##2 7
)##7 8
{$$ 
_context%% 
.%% 
Entry%% 
(%% 
order%% 
)%% 
.%% 
State%% #
=%%$ %
EntityState%%& 1
.%%1 2
Modified%%2 :
;%%: ;
await&& 
_context&& 
.&& 
SaveChangesAsync&& '
(&&' (
)&&( )
;&&) *
}'' 
public)) 

async)) 
Task)) 
DeleteOrderAsync)) &
())& '
int))' *
orderId))+ 2
)))2 3
{** 
var++ 
order++ 
=++ 
await++ 
_context++ "
.++" #
OrderTables++# .
.++. /
	FindAsync++/ 8
(++8 9
orderId++9 @
)++@ A
;++A B
if,, 

(,, 
order,, 
!=,, 
null,, 
),, 
{-- 	
_context.. 
... 
OrderTables..  
...  !
Remove..! '
(..' (
order..( -
)..- .
;... /
await// 
_context// 
.// 
SaveChangesAsync// +
(//+ ,
)//, -
;//- .
}00 	
}11 
public33 

async33 
Task33 
<33 
IEnumerable33 !
<33! "

OrderTable33" ,
>33, -
>33- .+
GetOrdersByCustomerAndDateAsync33/ N
(33N O
int33O R

customerId33S ]
,33] ^
DateTime33_ g
start33h m
,33m n
DateTime33o w
end33x {
)33{ |
{44 
return55 
await55 
_context55 
.55 
OrderTables55 )
.66 
Where66 
(66 
o66 
=>66 
o66 
.66 

CustomerId66 $
==66% '

customerId66( 2
&&663 5
o666 7
.667 8
	OrderDate668 A
>=66B D
start66E J
&&66K M
o66N O
.66O P
	OrderDate66P Y
<=66Z \
end66] `
)66` a
.77 
ToListAsync77 
(77 
)77 
;77 
}88 
public:: 

async:: 
Task:: 
DeleteOrderAsync:: &
(::& '

OrderTable::' 1
order::2 7
)::7 8
{;; 
_context<< 
.<< 
OrderTables<< 
.<< 
Remove<< #
(<<# $
order<<$ )
)<<) *
;<<* +
await== 
_context== 
.== 
SaveChangesAsync== '
(==' (
)==( )
;==) *
}>> 
}?? ¥
l/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Repositories/ProductsRepository.cs
public 
class 
ProductsRepository 
:  !
IProductsRepository" 5
{		 
private

 
readonly

 
MyDbContext

  
_context

! )
;

) *
public 

ProductsRepository 
( 
MyDbContext )
context* 1
)1 2
{ 
_context 
= 
context 
; 
} 
public 

async 
Task 
< 
IEnumerable !
<! "
Product" )
>) *
>* +
GetAllProductsAsync, ?
(? @
)@ A
{ 
return 
await 
_context 
. 
Products &
.& '
ToListAsync' 2
(2 3
)3 4
;4 5
} 
public 

async 
Task 
< 
Product 
> 
GetProductByIdAsync 2
(2 3
int3 6
	productId7 @
)@ A
{ 
return 
await 
_context 
. 
Products &
.& '
	FindAsync' 0
(0 1
	productId1 :
): ;
;; <
} 
public 

async 
Task 
AddProductAsync %
(% &
Product& -
product. 5
)5 6
{ 
_context 
. 
Products 
. 
Add 
( 
product %
)% &
;& '
await 
_context 
. 
SaveChangesAsync '
(' (
)( )
;) *
}   
public"" 

async"" 
Task"" 
UpdateProductAsync"" (
(""( )
Product"") 0
product""1 8
)""8 9
{## 
_context$$ 
.$$ 
Entry$$ 
($$ 
product$$ 
)$$ 
.$$  
State$$  %
=$$& '
EntityState$$( 3
.$$3 4
Modified$$4 <
;$$< =
await%% 
_context%% 
.%% 
SaveChangesAsync%% '
(%%' (
)%%( )
;%%) *
}&& 
public'' 

async'' 
Task'' 
<'' 
bool'' 
>'' 
ProductExistsAsync'' .
(''. /
int''/ 2
	productId''3 <
)''< =
{(( 
return)) 
await)) 
_context)) 
.)) 
Products)) &
.))& '
AnyAsync))' /
())/ 0
p))0 1
=>))2 4
p))5 6
.))6 7
	ProductId))7 @
==))A C
	productId))D M
)))M N
;))N O
}** 
public,, 

async,, 
Task,, 
DeleteProductAsync,, (
(,,( )
int,,) ,
	productId,,- 6
),,6 7
{-- 
var.. 
product.. 
=.. 
await.. 
_context.. $
...$ %
Products..% -
...- .
	FindAsync... 7
(..7 8
	productId..8 A
)..A B
;..B C
if// 

(// 
product// 
!=// 
null// 
)// 
{00 	
_context11 
.11 
Products11 
.11 
Remove11 $
(11$ %
product11% ,
)11, -
;11- .
await22 
_context22 
.22 
SaveChangesAsync22 +
(22+ ,
)22, -
;22- .
}33 	
}44 
}55 Œ
i/Users/tobiaspoulsen/RiderProjects/WebShopRestService2/WebShopRestService/Repositories/RolesRepository.cs
public 
class 
RolesRepository 
: 
IRolesRepository /
{		 
private

 
readonly

 
MyDbContext

  
_context

! )
;

) *
public 

RolesRepository 
( 
MyDbContext &
context' .
). /
{ 
_context 
= 
context 
; 
} 
public 

async 
Task 
< 
IEnumerable !
<! "
Role" &
>& '
>' (
GetAllRolesAsync) 9
(9 :
): ;
{ 
return 
await 
_context 
. 
Roles #
.# $
ToListAsync$ /
(/ 0
)0 1
;1 2
} 
public 

async 
Task 
< 
Role 
> 
GetRoleByIdAsync ,
(, -
int- 0
roleId1 7
)7 8
{ 
return 
await 
_context 
. 
Roles #
.# $
	FindAsync$ -
(- .
roleId. 4
)4 5
;5 6
} 
public 

async 
Task 
AddRoleAsync "
(" #
Role# '
role( ,
), -
{ 
_context 
. 
Roles 
. 
Add 
( 
role 
)  
;  !
await 
_context 
. 
SaveChangesAsync '
(' (
)( )
;) *
} 
public!! 

async!! 
Task!! 
UpdateRoleAsync!! %
(!!% &
Role!!& *
role!!+ /
)!!/ 0
{"" 
_context## 
.## 
Entry## 
(## 
role## 
)## 
.## 
State## "
=### $
EntityState##% 0
.##0 1
Modified##1 9
;##9 :
await$$ 
_context$$ 
.$$ 
SaveChangesAsync$$ '
($$' (
)$$( )
;$$) *
}%% 
public'' 

async'' 
Task'' 
DeleteRoleAsync'' %
(''% &
int''& )
roleId''* 0
)''0 1
{(( 
var)) 
role)) 
=)) 
await)) 
_context)) !
.))! "
Roles))" '
.))' (
	FindAsync))( 1
())1 2
roleId))2 8
)))8 9
;))9 :
if** 

(** 
role** 
!=** 
null** 
)** 
{++ 	
_context,, 
.,, 
Roles,, 
.,, 
Remove,, !
(,,! "
role,," &
),,& '
;,,' (
await-- 
_context-- 
.-- 
SaveChangesAsync-- +
(--+ ,
)--, -
;--- .
}.. 	
}// 
public22 

async22 
Task22 
<22 
bool22 
>22 
RoleExistsAsync22 +
(22+ ,
int22, /
roleId220 6
)226 7
{33 
return44 
await44 
_context44 
.44 
Roles44 #
.44# $
AnyAsync44$ ,
(44, -
r44- .
=>44/ 1
r442 3
.443 4
RoleId444 :
==44; =
roleId44> D
)44D E
;44E F
}55 
}66 