import random as rnd

posible_relations = "P1,P2,P3,>,=:P1,P2,P3,=,>:P1,P2,>,P3,=:P1,P2,>,=,P3:P1,P2,=,P3,>:P1,P2,=,>,P3:P1,P3,P2,>,=:P1,P3,P2,=,>:P1,P3,>,P2,=:P1,P3,>,=,P2:P1,P3,=,P2,>:P1,P3,=,>,P2:P1,>,P2,P3,=:P1,>,P2,=,P3:P1,>,P3,P2,=:P1,>,P3,=,P2:P1,>,=,P2,P3:P1,>,=,P3,P2:P1,=,P2,P3,>:P1,=,P2,>,P3:P1,=,P3,P2,>:P1,=,P3,>,P2:P1,=,>,P2,P3:P1,=,>,P3,P2:P2,P1,P3,>,=:P2,P1,P3,=,>:P2,P1,>,P3,=:P2,P1,>,=,P3:P2,P1,=,P3,>:P2,P1,=,>,P3:P2,P3,P1,>,=:P2,P3,P1,=,>:P2,P3,>,P1,=:P2,P3,>,=,P1:P2,P3,=,P1,>:P2,P3,=,>,P1:P2,>,P1,P3,=:P2,>,P1,=,P3:P2,>,P3,P1,=:P2,>,P3,=,P1:P2,>,=,P1,P3:P2,>,=,P3,P1:P2,=,P1,P3,>:P2,=,P1,>,P3:P2,=,P3,P1,>:P2,=,P3,>,P1:P2,=,>,P1,P3:P2,=,>,P3,P1:P3,P1,P2,>,=:P3,P1,P2,=,>:P3,P1,>,P2,=:P3,P1,>,=,P2:P3,P1,=,P2,>:P3,P1,=,>,P2:P3,P2,P1,>,=:P3,P2,P1,=,>:P3,P2,>,P1,=:P3,P2,>,=,P1:P3,P2,=,P1,>:P3,P2,=,>,P1:P3,>,P1,P2,=:P3,>,P1,=,P2:P3,>,P2,P1,=:P3,>,P2,=,P1:P3,>,=,P1,P2:P3,>,=,P2,P1:P3,=,P1,P2,>:P3,=,P1,>,P2:P3,=,P2,P1,>:P3,=,P2,>,P1:P3,=,>,P1,P2:P3,=,>,P2,P1:>,P1,P2,P3,=:>,P1,P2,=,P3:>,P1,P3,P2,=:>,P1,P3,=,P2:>,P1,=,P2,P3:>,P1,=,P3,P2:>,P2,P1,P3,=:>,P2,P1,=,P3:>,P2,P3,P1,=:>,P2,P3,=,P1:>,P2,=,P1,P3:>,P2,=,P3,P1:>,P3,P1,P2,=:>,P3,P1,=,P2:>,P3,P2,P1,=:>,P3,P2,=,P1:>,P3,=,P1,P2:>,P3,=,P2,P1:>,=,P1,P2,P3:>,=,P1,P3,P2:>,=,P2,P1,P3:>,=,P2,P3,P1:>,=,P3,P1,P2:>,=,P3,P2,P1:=,P1,P2,P3,>:=,P1,P2,>,P3:=,P1,P3,P2,>:=,P1,P3,>,P2:=,P1,>,P2,P3:=,P1,>,P3,P2:=,P2,P1,P3,>:=,P2,P1,>,P3:=,P2,P3,P1,>:=,P2,P3,>,P1:=,P2,>,P1,P3:=,P2,>,P3,P1:=,P3,P1,P2,>:=,P3,P1,>,P2:=,P3,P2,P1,>:=,P3,P2,>,P1:=,P3,>,P1,P2:=,P3,>,P2,P1:=,>,P1,P2,P3:=,>,P1,P3,P2:=,>,P2,P1,P3:=,>,P2,P3,P1:=,>,P3,P1,P2:>,>,P2,P3,P1:>,>,P2,P1,P3:>,>,P3,P2,P1:>,>,P3,P1,P2:>,>,P1,P2,P3:>,>,P1,P3,P2:>,P2,>,P3,P1:>,P2,>,P1,P3:>,P2,P3,>,P1:>,P2,P3,P1,>:>,P2,P1,>,P3:>,P2,P1,P3,>:>,P3,>,P2,P1:>,P3,>,P1,P2:>,P3,P2,>,P1:>,P3,P2,P1,>:>,P3,P1,>,P2:>,P3,P1,P2,>:>,P1,>,P2,P3:>,P1,>,P3,P2:>,P1,P2,>,P3:>,P1,P2,P3,>:>,P1,P3,>,P2:>,P1,P3,P2,>:P2,>,>,P3,P1:P2,>,>,P1,P3:P2,>,P3,>,P1:P2,>,P3,P1,>:P2,>,P1,>,P3:P2,>,P1,P3,>:P2,P3,>,>,P1:P2,P3,>,P1,>:P2,P3,P1,>,>:P2,P1,>,>,P3:P2,P1,>,P3,>:P2,P1,P3,>,>:P3,>,>,P2,P1:P3,>,>,P1,P2:P3,>,P2,>,P1:P3,>,P2,P1,>:P3,>,P1,>,P2:P3,>,P1,P2,>:P3,P2,>,>,P1:P3,P2,>,P1,>:P3,P2,P1,>,>:P3,P1,>,>,P2:P3,P1,>,P2,>:P3,P1,P2,>,>:P1,>,>,P2,P3:P1,>,>,P3,P2:P1,>,P2,>,P3:P1,>,P2,P3,>:P1,>,P3,>,P2:P1,>,P3,P2,>:P1,P2,>,>,P3:P1,P2,>,P3,>:P1,P2,P3,>,>:P1,P3,>,>,P2:P1,P3,>,P2,>:P1,P3,P2,>,>:P1,=,P2,=,P3"

relations = posible_relations.split(":");

valid_relation = []

for relation in relations:
    symbols = relation.split(",")
    if("P" in symbols[0]):
        if(len(symbols[1]) == 1 and len(symbols[3]) == 1): 
            valid_relation.append(" ".join(symbols))


particle_settings = ["1,-1,1", "1,1,1", "1,-1,-1", "1,1,-1", "-1,-1,-1", "-1,-1,1", "-1,1,-1", "-1,1,1"]

random_pairing = {}

rnd.shuffle(particle_settings)

for i in range(6):  
    random_pairing[particle_settings[i]] = rnd.choice(valid_relation)
    valid_relation.remove(random_pairing[particle_settings[i]])


count = 0 
letters = ["A", "B", "C", "D"]

for setting in random_pairing.keys():

    count+=1
    relation = random_pairing[setting]
    particles = setting.split(",")

    print("{")
    print(f'''
        "relation": "{relation}", 
        "setting": {{
            "x": {particles[0]},
            "y": {particles[1]},
            "z": {particles[2]}
        }},
        "images": [
            "figure_{count}_A",
            "figure_{count}_B",
            "figure_{count}_C",
            "figure_{count}_D"
        ],
        "models": [
            "model_{count}_A",
            "model_{count}_B",
            "model_{count}_C",
            "model_{count}_D",
        ],
        "correctImage": "figure_{count}_{rnd.choice(letters)}",
        "correctModel": "model_{count}_{rnd.choice(letters)}",
        ''')
    print("},")
