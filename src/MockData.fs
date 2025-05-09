module App.MockData


let CreColumn =
    {
        Name = "Cre ID"
        Description = """
The "Cre number" is a standardized gene identifier used in the Chlamydomonas reinhardtii genome database. It follows the format: `CreYY.gNNNNNN`

- `Cre` — stands for Chlamydomonas reinhardtii
- `YY` — chromosome number (e.g., Cre01 = chromosome 1)
- `gNNNNNN` — unique gene identifier on that chromosome

"""
        Values = [|
            "Cre01.g014600"
            "Cre01.g021000"
            "Cre01.g034500"
            "Cre02.g120000"
            "Cre02.g091200"
            "Cre03.g189950"
            "Cre03.g194300"
            "Cre04.g215600"
            "Cre04.g219500"
            "Cre05.g238800"
            "Cre05.g245100"
            "Cre06.g271150"
            "Cre06.g282800"
            "Cre06.g297500"
            "Cre07.g308600"
            "Cre07.g317600"
            "Cre08.g367400"
            "Cre08.g384250"
            "Cre09.g390900"
            "Cre09.g398700"
            "Cre10.g429950"
            "Cre10.g440200"
            "Cre10.g452800"
            "Cre11.g467100"
            "Cre11.g471900"
            "Cre12.g485550"
            "Cre12.g492800"
            "Cre13.g558300"
            "Cre13.g573700"
            "Cre13.g591950"
            "Cre14.g598800"
            "Cre14.g605700"
            "Cre15.g629100"
            "Cre15.g639250"
            "Cre16.g651800"
            "Cre16.g663600"
            "Cre16.g676550"
            "Cre17.g700900"
            "Cre17.g703200"
            "Cre17.g709800"
        |]
    }

let MapManColumn =
    {
        Name = "MapMan Bins"
        Description = """hierarchical categories used to classify genes by biological function in Chlamydomonas reinhardtii and other plants."""
        Values = [|
    "1.1.1.1"      // Photosynthesis: light reactions: Photosystem II
    "1.1.1.2"      // Photosynthesis: light reactions: Photosystem I
    "1.1.2"        // Photosynthesis: Calvin cycle
    "1.2.1"        // Major CHO metabolism: sucrose synthesis
    "1.2.3"        // Major CHO metabolism: glycolysis
    "1.3.1"        // Minor CHO metabolism: raffinose family
    "2.1.1"        // Mitochondrial electron transport / ATP synthesis: Complex I
    "2.2.2"        // TCA cycle: malate dehydrogenase
    "3.1.1"        // Cell wall: cellulose synthesis
    "3.2.2"        // Lipid metabolism: phospholipid synthesis
    "4.1.1"        // Nitrogen metabolism: nitrate reduction
    "4.2.1"        // Sulfur metabolism: sulfate transport
    "5.1.1"        // Amino acid metabolism: synthesis: glutamate family
    "5.1.3"        // Amino acid metabolism: degradation: branched chain AA
    "6.1"          // Secondary metabolism: isoprenoids
    "6.2"          // Secondary metabolism: phenylpropanoids
    "7.1.1"        // Co-factor metabolism: thiamine biosynthesis
    "7.2.1"        // Vitamin metabolism: B6 synthesis
    "8.1.2"        // Hormone metabolism: auxin synthesis
    "8.2.1"        // Hormone metabolism: ABA degradation
    "9.1.1"        // Stress: abiotic: heat
    "9.1.2"        // Stress: abiotic: cold
    "9.2.1"        // Stress: biotic: pathogen recognition
    "10.1"         // Development: flowering
    "10.2"         // Development: embryogenesis
    "11.1"         // RNA processing: transcription factors
    "11.2"         // RNA processing: RNA splicing
    "12.1"         // Protein synthesis: ribosomal proteins
    "12.2"         // Protein degradation: ubiquitin pathway
    "13.1.1"       // Signaling: calcium signaling
    "13.2"         // Signaling: protein kinases
    "14.1"         // Transport: nutrient uptake
    "14.2.2"       // Transport: mitochondrial transporters
    "15.1"         // Cell cycle: DNA replication
    "15.3"         // Cell cycle: cytokinesis
    "16.1"         // DNA repair: mismatch repair
    "17.1"         // Chromatin structure: histones
    "18.1"         // Cytoskeleton: microtubule-based movement
    "19.1"         // Vesicle transport: endocytosis
    "20.1"         // Miscellaneous: unknown proteins
|]
    }

let CustomMapping =
    {
        Name = "Custom Mapping"
        Description = "Custom mapping of genes to specific categories."
        Values = [|
            "map01"
            "map02"
            "map03; map07"
            "map04"
            "map05; map06; map09"
            "map08"
            "map10"
            "map11"
            "map12; map13"
            "map14"
            "map15; map16"
            "map17"
            "map18; map19; map20"
            "map21"
            "map22"
            "map23"
            "map24; map25"
            "map26"
            "map27"
            "map28; map29"
            "map30"
            "map31; map32; map33"
            "map34"
            "map35"
            "map36"
            "map37"
            "map38; map39"
            "map40; map41"
            "map42"
            "map43"
            "map44"
            "map45; map46"
            "map47"
            "map48; map49; map50"
            "map51"
            "map52"
            "map53; map54"
            "map55"
            "map56"
            "map57"
            |]
    }

let MapFile =
    {
        Columns =
            [|
                CreColumn
                MapManColumn
                CustomMapping
            |]
    }