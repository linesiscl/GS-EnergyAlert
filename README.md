# EnergyAlert - Global Solution: C# Software Development 

O projeto é uma aplicação em C# que serve como um sistema de alerta comunitário para falhas e quedas de energia. Seu principal objetivo é de ajudar comunidades a se organizarem em relação às quedas de energia e manterem-se informados sobre os locais onde podem ocorrer mais riscos, ou onde os problemas já foram resolvidos.

---

## 🌨️ Integrantes

Aline Fernandes - RM97966

Camilly Ishida - RM551474

Julia Leite - RM550201

---

## 🌨️ Descrição do problema

Atualmente, a população de diversos países precisa lidar constantemente com as quedas de energia em detrimento de fortes chuvas, falto de preparo, entre outros motivos. De acordo com o portal G1 (2025), apenas em 2024 foram feitos 5,7 milhões de chamados relativos à falta de luz no Brasil, além de o tempo de resposta para atendimentos de situações de emergência ter deteriorado.

Por consequência do ritmo acelerado e dependente das fontes energéticas em que vivemos, muitas pessoas precisam da energia para necessidades essenciais, como trabalhar ou estudar, sem contar os que precisam dela para, de fato, sobreviver, como no caso de pessoas que utilizam aparelhos hospitalares.

Em suma, a falta de energia é um problema que atinge diversos cidadãos em diversas escalas, e por isso, é necessário que a tecnologia possa ajudar na prevenção, solução ou auxílio nessas ocasiões.

---

## 🌨️ Visão Geral da Solução


### 🌨️ Instruções de Execução
Ao testar o projeto, mudar o caminho (dentro das classes Logs.cs, RegistroFalhas.cs e CadastroOuLogin.cs dentro da pasta Service) dos arquivos para o caminho em que eles se encontram em seu computador.

---
## 🌨️ Estrutura de pastas

```
📁 GlobalSolution
├── 📁 Data
│   ├── admin.json
│   ├── alertas.json
│   ├── cidadaos.json
│   ├── falhas.json
│   └── tecnicos.json
├── 📁 Model
│   ├── Administrador.cs
│   ├── Alerta.cs
│   ├── Cidadao.cs
│   ├── FalhaDeEnergia.cs
│   ├── IRegistroDeOcorrencia.cs
│   ├── IUsuaria.cs
│   └── Tecnico.cs
├── 📁 Service
│   ├── CadastroOuLogin.cs
│   ├── Logs.cs
│   ├── RegistroFalhas.cs
│   └── ServicoDeAlerta.cs
├── logs.txt
└── Program.cs
```
