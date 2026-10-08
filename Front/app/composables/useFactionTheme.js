import { onMounted, watch } from 'vue'

const STORAGE_KEY = 'wze_faction_theme'

const factionThemes = {
    brotherhood: {
        light: { background: '#EEE9D9', surface: '#F8F5EA', surfaceAlt: '#E4DECA', primary: '#8B702C', accent: '#B52F35', text: '#252421', mutedText: '#716C5D', border: '#CFC7AE' },
        dark: { background: '#121312', surface: '#1D1D1A', surfaceAlt: '#282720', primary: '#C5A858', accent: '#B7353B', text: '#EEE9D9', mutedText: '#A7A18D', border: '#3D392C' },
    },
    capitol: {
        light: { background: '#E9EDF0', surface: '#F8FAFB', surfaceAlt: '#DCE3E8', primary: '#316A9A', accent: '#C28A32', text: '#20282E', mutedText: '#65727C', border: '#C3CDD4' },
        dark: { background: '#11171C', surface: '#1B242B', surfaceAlt: '#253039', primary: '#4D91C4', accent: '#D0A04A', text: '#E8EEF2', mutedText: '#94A5AF', border: '#35434C' },
    },
    bauhaus: {
        light: { background: '#E9E5D8', surface: '#F6F3E9', surfaceAlt: '#DCD7C5', primary: '#486746', accent: '#A28443', text: '#252821', mutedText: '#697064', border: '#C8C1AC' },
        dark: { background: '#131914', surface: '#1D261E', surfaceAlt: '#283328', primary: '#66895E', accent: '#B89A51', text: '#E7E4D8', mutedText: '#9CA695', border: '#394639' },
    },
    mishima: {
        light: { background: '#E9E3DD', surface: '#F8F5F1', surfaceAlt: '#DED6CF', primary: '#8F2429', accent: '#B98A32', text: '#171616', mutedText: '#686260', border: '#C8BDB4' },
        dark: { background: '#110F10', surface: '#1D1718', surfaceAlt: '#281D1E', primary: '#B83238', accent: '#D2A342', text: '#EEE7DF', mutedText: '#A79B92', border: '#423031' },
    },
    imperial: {
        light: { background: '#E3E1DC', surface: '#F1F0EC', surfaceAlt: '#D3D3CF', primary: '#4E5961', accent: '#963A35', text: '#242728', mutedText: '#6B7070', border: '#BCBFBE' },
        dark: { background: '#151719', surface: '#202426', surfaceAlt: '#2A3033', primary: '#78858C', accent: '#AD4039', text: '#E4E5E2', mutedText: '#9BA1A1', border: '#3B4143' },
    },
    cybertronic: {
        light: { background: '#E7ECEC', surface: '#F6F9F9', surfaceAlt: '#D7E0E1', primary: '#247D8B', accent: '#39AFC0', text: '#172124', mutedText: '#617174', border: '#BFCBCD' },
        dark: { background: '#0C1416', surface: '#141F22', surfaceAlt: '#1B292D', primary: '#2F98A8', accent: '#51D1DF', text: '#E3F0F1', mutedText: '#8EA8AC', border: '#2C4145' },
    },
    algeroth: {
        light: { background: '#E5DCDA', surface: '#F4EEEC', surfaceAlt: '#D9CCCA', primary: '#61262C', accent: '#A62E37', text: '#24191A', mutedText: '#715F5F', border: '#C8B8B6' },
        dark: { background: '#120E0F', surface: '#1E1416', surfaceAlt: '#291A1D', primary: '#812932', accent: '#C0444D', text: '#E9DDDA', mutedText: '#A89491', border: '#42282B' },
    },
    ilian: {
        light: { background: '#E1E7EC', surface: '#F3F6F8', surfaceAlt: '#D0DAE2', primary: '#354F72', accent: '#6DA8D0', text: '#1D252C', mutedText: '#63717D', border: '#B8C5CF' },
        dark: { background: '#0B1119', surface: '#121D29', surfaceAlt: '#1B2938', primary: '#456B98', accent: '#78B7E3', text: '#E2EBF2', mutedText: '#91A5B5', border: '#2B3C4E' },
    },
    muawijhe: {
        light: { background: '#E8E2EA', surface: '#F6F2F8', surfaceAlt: '#D9D0DE', primary: '#654575', accent: '#9B65B5', text: '#241D28', mutedText: '#716778', border: '#C7BBCB' },
        dark: { background: '#110D15', surface: '#1C1421', surfaceAlt: '#281B30', primary: '#70478A', accent: '#B26BD0', text: '#EDE5F0', mutedText: '#A597AC', border: '#402E49' },
    },
    demnogonis: {
        light: { background: '#E4E5D8', surface: '#F3F4E9', surfaceAlt: '#D4D6C3', primary: '#596438', accent: '#92983C', text: '#25271D', mutedText: '#6E725D', border: '#C1C4AE' },
        dark: { background: '#10130D', surface: '#1A2014', surfaceAlt: '#242B19', primary: '#637239', accent: '#A8AD42', text: '#E5E7D8', mutedText: '#9DA18A', border: '#384126' },
    },
    darkLegion: {
        light: { background: '#E2DEE4', surface: '#F1EEF2', surfaceAlt: '#D4CDD8', primary: '#51405C', accent: '#96363D', text: '#211D22', mutedText: '#6E6671', border: '#C0B7C4' },
        dark: { background: '#100D12', surface: '#19131D', surfaceAlt: '#241B29', primary: '#674A78', accent: '#A93640', text: '#EAE2EC', mutedText: '#A097A5', border: '#3A2D42' },
    },
    darkLegionMixed: {
        light: { background: '#E4E0DA', surface: '#F3F0EB', surfaceAlt: '#D7D2CB', primary: '#4B4446', accent: '#9B4145', text: '#211E1E', mutedText: '#706A67', border: '#C2BCB5' },
        dark: { background: '#111011', surface: '#1C1A1B', surfaceAlt: '#272425', primary: '#71696A', accent: '#B44747', text: '#E8E3DD', mutedText: '#A09A96', border: '#3A3636' },
    },
}

function displayFactionName(faction) {
    return faction.replace(/([a-z])([A-Z])/g, '$1 $2').replace(/\b\w/g, letter => letter.toUpperCase())
}

export function useFactionTheme() {
    const faction = useState('wze-faction-theme', () => 'capitol')
    const colorMode = useColorMode()
    const options = Object.keys(factionThemes).map(value => ({
        value,
        label: displayFactionName(value),
    }))

    function applyTheme() {
        if (!import.meta.client) return

        const selectedFaction = factionThemes[faction.value] ? faction.value : 'capitol'
        const mode = colorMode.value === 'dark' ? 'dark' : 'light'
        const root = document.documentElement
        root.dataset.factionTheme = selectedFaction

        for (const [name, value] of Object.entries(factionThemes[selectedFaction][mode])) {
            const variable = name.replace(/[A-Z]/g, letter => `-${letter.toLowerCase()}`)
            root.style.setProperty(`--theme-${variable}`, value)
        }
    }

    onMounted(() => {
        const storedFaction = localStorage.getItem(STORAGE_KEY)
        if (storedFaction && factionThemes[storedFaction]) faction.value = storedFaction
        applyTheme()
    })

    watch([faction, () => colorMode.value], () => {
        applyTheme()
        if (import.meta.client) localStorage.setItem(STORAGE_KEY, faction.value)
    })

    return { faction, options }
}
