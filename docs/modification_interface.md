# game language

## syntax

<pre>
if (2 + (5 / 2) > 4; "sample"; 'expression') -> "sample"
a := 1 -> a (assign, return variable name)
a := a + 1
b := 3 / a
b -> 1 (in this context)
</pre>

Func_OR_var_NamE = func_or_var_name, but func_or_var_name - origin

### types

| types   | sample                          |
|---------|---------------------------------|
| string  | 'sample string' or "another"    |
| float   | 123 or 1.23 or 1,23 (8 byte)    |
| boolean | (regex pattern) ^(true/yes/on)$ |
| array   | \[-3; 5 / 2; 2 * 2]             |

### operations

| name | sample                 |
|------|------------------------|
| and  | 1 = 1 and true -> true |
| or   | 1 = 1 or off -> true   |
| =    | 5 / 2 = 2 -> true      |
| <    | 1 < 2 -> true          |
| \>   | 3 > 5 -> false         |
| +    | 2 + 5 -> 7             |
| -    | 3 - -5 -> -2           |
| *    | 16 * 16 -> 256         |
| /    | 5 / 2 -> 2.5           |

## global context

### functions

#### internal

| name  | sample                                                     |
|-------|------------------------------------------------------------|
| if    | if (1=1;5;7) -> 5 or if (false;"smth") -> (empty string)   |
| not   | not(1 = 2) -> true or not(on) -> false                     |
| min   | min(4; 2 / 2; 3) -> 1 or min() -> -1.798E+308              |
| max   | max(2 + 1) -> 3 or max() -> 1.798E+308                     |
| log   | log("a="; 10) -> (out to stream a=10)                      |
| error | error("2"; "+"; "3="; 5) -> (like log, but stop thread)    |

#### external

| name                | input                                         | output |
|---------------------|-----------------------------------------------|--------|
| spawn_interval_fire | (string) config_name; (array2) spawn_position | void   |

### variables

| name | value    |
|------|----------|
| pi   | 3.141593 |

# config

* parent -> name of inherited config
* string = script

## hooks

### unit

| type      | name          | default                                   |
|-----------|---------------|-------------------------------------------|
| script    | on_damage     | set_enemy_hp(get_enemy_hp() - own_damage) |
| float     | speed         | 1.0                                       |
| int       | damage        | 1                                         |
| float     | attack_radius | 5.0                                       |
| float\[4] | bullet_color  | \[255, 255, 255] (default alpha 255)      |

#### on_damage

call on bullet collide

<details>
<summary> local context </summary>
<br>

<b> variables </b>

| type   | name         |
|--------|--------------|
| string | own_name     |
| string | enemy_name   |
| int    | own_damage   |
| int    | enemy_damage |

<b> function </b>

| type | name         |
|------|--------------|
| int  | get_own_hp   |
| void | set_own_hp   |
| int  | get_enemy_hp |
| void | set_enemy_hp |

</details>
